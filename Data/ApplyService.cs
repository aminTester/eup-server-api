using EUP.Data.Enums;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using EUP.Shared;

namespace EUP.Data
{
    public interface IApplyService 
    {
       Task< IEnumerable<ProffesorModel>> GetAllProffesors(/*CountryCode country*/);
        Task<ProffesorModel> GetProffesor(int ProfId);

        Task<ProffesorEditModel> UpdateProf(int profId, ProffesorEditModel profModel);
    }
    public class ApplyService : IApplyService
    {
        private ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ApplyService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async  Task< IEnumerable<ProffesorModel>> GetAllProffesors(/*CountryCode country*/)
        {

            CountryCode country = CountryCode.Australia;
            try
            {
                
                var data = _context.Proffesors.Include(b => b.university).Where(p=>p.university.Country== country).OrderBy(p => p.university!.Country).ThenBy(p => p.university!.ID).ToList();

                IEnumerable<ProffesorModel> myProfs = _mapper.Map<IEnumerable<Proffesor>, IEnumerable<ProffesorModel>>(data);
                return await Task.FromResult(myProfs);
            }
            catch (Exception ex)
            {

                return null;
            }

        }

        public async Task<ProffesorModel> GetProffesor(int ProfId)
        {
            try
            {
                ProffesorModel pModel=_mapper.Map<Proffesor,ProffesorModel>(
                    await _context.Proffesors.FirstOrDefaultAsync( x=>x.ID==ProfId));
                return pModel;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ProffesorEditModel> UpdateProf(int profId, ProffesorEditModel profModel)
        {
            try
            {
                if (profId != profModel.Id)
                {
                    return null; // IDs do not match, return null.
                }

                var profDetail = await _context.Proffesors.FindAsync(profId);
                if (profDetail == null)
                {
                    return null; // Professor not found, return null.
                }

                // Map only if the existing entity is found
                _mapper.Map(profModel, profDetail);
                profDetail.UpdateDate = DateTime.Now;

                _context.Proffesors.Update(profDetail);
                await _context.SaveChangesAsync();

                return _mapper.Map<Proffesor, ProffesorEditModel>(profDetail);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating professor: {ex.Message}");
                return null; // Return null instead of throwing
            }
        }
    }
}
