using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerLibrary.Models
{
    /// <summary>
    /// Represents one person.
    /// </summary>
    public class PersonModel
    {
        public int ID { get; set; }
        /// <summary>
        /// The first name of the Person
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The last name of the Person
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The primary email address of the Person
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// The primary phone number of the person
        /// </summary>
        public string PhoneNumber { get; set; }

        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

    }
}
