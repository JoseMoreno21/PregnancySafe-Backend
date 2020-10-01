﻿using PregnancySafe.Domain.Models;
using PregnancySafe.Domain.Repositories;
using PregnancySafe.Domain.Services;
using PregnancySafe.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PregnancySafe.Services
{
    public class ObstetricianService : IObstetricianService
    {
        private readonly IObstetricianRepository _obstetricianRepository;
        public ObstetricianService(IObstetricianRepository obstetricianRepository)
        {
            _obstetricianRepository = obstetricianRepository;
        }

        public async Task<ObstetricianResponse> DeleteAsync(int id)
        {
            var existingObstetrician = await _obstetricianRepository.FindByIdAsync(id);

            if (existingObstetrician == null)
                return new ObstetricianResponse("Obstetrician not found");

            try
            {
                _obstetricianRepository.Remove(existingObstetrician);
                return new ObstetricianResponse(existingObstetrician);
            }
            catch (Exception exception)
            {
                return new ObstetricianResponse($"An error ocurred while deleting the Obstetrician: {exception.Message}");
            }
        }

        public async Task<IEnumerable<Obstetrician>> ListAsync()
        {
            return await _obstetricianRepository.ListAsync();
        }

        public async Task<ObstetricianResponse> SaveAsync(Obstetrician obstetrician)
        {
            try
            {
                await _obstetricianRepository.AddASync(obstetrician);
                return new ObstetricianResponse(obstetrician);
            }
            catch (Exception exception)
            {
                return new ObstetricianResponse($"An error ocurred while saving the Obstetrician: {exception.Message}");
            }
        }

        public async Task<ObstetricianResponse> UpdateAsync(int id, Obstetrician obstetrician)
        {
            var existingObstetrician = await _obstetricianRepository.FindByIdAsync(id);

            if (existingObstetrician == null)
                return new ObstetricianResponse("Obstetrician not found");

            try
            {
                _obstetricianRepository.Update(existingObstetrician);
                return new ObstetricianResponse(existingObstetrician);
            }
            catch (Exception exception)
            {
                return new ObstetricianResponse($"An error ocurred while updating the Obstetrician: {exception.Message}");
            }
        }
    }
}
