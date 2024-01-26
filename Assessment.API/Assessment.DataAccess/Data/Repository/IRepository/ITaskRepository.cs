using Assessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.DataAccess.Data.Repository.IRepository
{
    public interface ITaskRepository
    {
        IEnumerable<TaskModel> GetAll();
        TaskModel GetById(int id);
        void Insert(TaskModel task);
        void Update(TaskModel task);
        void Delete(int ID);
        void Save();
    }
}
