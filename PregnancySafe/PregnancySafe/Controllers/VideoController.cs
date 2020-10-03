﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PregnancySafe.Domain.Models;
using PregnancySafe.Domain.Services;
using PregnancySafe.Extensions;
using PregnancySafe.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PregnancySafe.Controllers
{
    [Route("/api/[controller]")]
    public class VideoController : Controller
    {
        private readonly IVideoService _videoService;
        private readonly IMapper _mapper;
        public VideoController(IVideoService videoService, IMapper mapper)
        {
            _videoService = videoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<VideoResource>> GetAllAsync()
        {
            var videos = await _videoService.ListAsync();
            var resources = _mapper
                .Map<IEnumerable<Video>, IEnumerable<VideoResource>>(videos);
            return resources;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveVideoResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            var video = _mapper.Map<SaveVideoResource, Video>(resource);
            var result = await _videoService.SaveAsync(video);

            if (!result.Success)
                return BadRequest(result.Message);

            var videoResource = _mapper.Map<Video, VideoResource>(result.Video);
            return Ok(videoResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, SaveVideoResource resource)
        {
            var video = _mapper.Map<SaveVideoResource, Video>(resource);
            var result = await _videoService.UpdateAsync(id, video);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            var videoResource = _mapper.Map<Video, VideoResource>(result.Video);
            return Ok(videoResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _videoService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);
            var videoResource = _mapper.Map<Video, VideoResource>(result.Video);
            return Ok(videoResource);
        }
    }
}
