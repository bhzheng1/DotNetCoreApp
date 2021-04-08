using CsvHelper;
using Microsoft.Extensions.Configuration;
using One.DAL.Repositories;
using One.Domain.Mapping;
using One.Domain.Models;
using One.Domain.ServiceRequest;
using One.Domain.ServiceResponse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace One.Business.hr.Country
{
    public class CountryBusiness : ICountryBusiness
    {
        private readonly ICountryRepository _countryRepository;
        private readonly string _singleFileProcessFolder;
        private readonly IUtility _utility;
        private ILogger<CountryBusiness> _logger;

        public CountryBusiness(ICountryRepository countryRepository, IConfiguration config,IUtility utility, ILogger<CountryBusiness> logger)
        {
            _countryRepository = countryRepository;
            _utility = utility;
            _logger = logger;
            _singleFileProcessFolder = config.GetSection("StoreFileSettings")["SingleFileProcessFolder"];
        }

        public async Task<ServiceResponse<IEnumerable<CountryReadModel>>> GetAll(CountryRequest request)
        {
            var result = await _countryRepository.GetAllCountries();
            return ServiceResponse.FromResult(result);
        }

        public async Task<Stream> DownloadAllCountry(CountryDownloadRequest request)
        {
            try
            {
                var result = await _countryRepository.GetAllCountries();
                var randomProcessId = Guid.NewGuid().ToString().Substring(0, 5);
                var currentProcessPath = Path.Combine(_singleFileProcessFolder, randomProcessId);
                var fileFullPath = Path.Combine(currentProcessPath, "Country.csv");

                if (Directory.Exists(currentProcessPath))
                {
                    Directory.Delete(currentProcessPath);
                    Directory.CreateDirectory(currentProcessPath);
                }

                using (var writer = new StreamWriter(fileFullPath, false, System.Text.Encoding.UTF8))
                using (var csv = new CsvWriter(writer))
                {
                    csv.Configuration.RegisterClassMap<ExportCountryMapper>();
                    csv.WriteRecords(result);
                }

                var stream = new MemoryStream();
                using (var fileStream = new FileStream(fileFullPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    await fileStream.CopyToAsync(stream);
                }

                _utility.DeleteFolder(currentProcessPath);
                stream.Position = 0;
                return stream;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }
        }
    }
}
