using System;
using System.Collections.Generic;
using System.Text;

namespace Multithreading2
{
    public class User1
    {
        protected readonly int _id = 0; //Note: added read only
        public readonly string _name; //Note: added read only

        //Note: dependency injection onstead of another method
        public User1(int id, string name, string location, int designation)
        {
            _name = name;
            Location = location;
            _id = id;
            Designation = designation;
        }        
        
        public string GetUserDetails(int uid, string userName)
        {
            return $"{_id} - {uid} - {userName} - {_name}";
        }

        public int Designation { get; } 
        public string Location { get; } 
    }
    
}
