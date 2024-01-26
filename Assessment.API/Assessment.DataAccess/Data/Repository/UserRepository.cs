using Assessment.DataAccess.Data.Repository.IRepository;
using Assessment.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.DataAccess.Data.Repository
{
    public class UserRepository: IUserRepository
    {

        //The following variable is going to hold the ApplicationDbContext instance
        private readonly ApplicationDbContext _context;
        //Initializing the ApplicationDbContext instance which it received as an argument
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        //This method will return all the user from the user table
        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }
        //This method will return one user's information from the user table
        //based on the user which it received as an argument
        public User GetById(int id)
        {
            return _context.Users.Find(id);
        }
        //This method will Insert one user object into the user table
        //It will receive the user object as an argument which needs to be inserted into the database
        public void Insert(User user)
        {
            //The State of the Entity is going to be Added State
            _context.Users.Add(user);
        }
        //This method is going to update the user data in the database
        //It will receive the user object as an argument
        public void Update(User user)
        {
            //It will mark the Entity State as Modified
            _context.Entry(user).State = EntityState.Modified;
        }
        //This method is going to remove the user Information from the Database
        //It will receive the user as an argument whose information needs to be removed from the database
        public void Delete(int ID)
        {
            //First, fetch the user details based on the user id
            var user = _context.Users.Find(ID);

            //If the user object is not null, then remove the employee
            if (user != null)
            {
                //This will mark the Entity State as Deleted
                _context.Users.Remove(user);
            }

        }
        //This method will make the changes permanent in the database
        //That means once we call Insert, Update, and Delete Methods, then we need to call
        //the Save method to make the changes permanent in the database
        public void Save()
        {
            //Based on the Entity State, it will generate the corresponding SQL Statement and
            //Execute the SQL Statement in the database
            //For Added Entity State: It will generate INSERT SQL Statement
            //For Modified Entity State: It will generate UPDATE SQL Statement
            //For Deleted Entity State: It will generate DELETE SQL Statement
            _context.SaveChanges();
        }

        private bool disposed = false;
        //As a context object is a heavy object or you can say time-consuming object
        //So, once the operations are done we need to dispose of the same using Dispose method
        //The EmployeeDBContext class inherited from DbContext class and the DbContext class
        //is Inherited from the IDisposable interface
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
