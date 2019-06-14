using ATG.DAL.DatabaseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ATG.DAL
{
    public interface IATGRepository
    {
        Task<int> CreateNewVisitor(ATGVisitor visitor);
        Task<bool> UpdateByID(ATGVisitor visitor);
        Task<List<ATGVisitor>> GetAllVisitors();
    }
}
