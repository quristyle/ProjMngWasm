using System;
using ProjMngWasm.Models;

namespace ProjMngWasm.Services;

public class UniversityService : BaseService, IUniversityService {
        public UniversityService(HttpClient httpClient) : base(httpClient) { }

        public async Task<IEnumerable<University>> GetUniversities() {
            return await GetMethodList<University>($"search?country=Korea,+Republic+of");
        }
    }






public interface IUniversityService {
        Task<IEnumerable<University>> GetUniversities();
    }
