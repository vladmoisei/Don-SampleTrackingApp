using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Don_SampleTrackingApp
{
    public class TrackingUser
    {
        public int UserId { get; set; }
        [Required]
        [MaxLength(100)]
        public string UserName { get; set; }
        [MaxLength(100)]
        public string Password { get; set; }
        [MaxLength(100)]
        public string Nume { get; set; }
        [MaxLength(100)]
        public string Prenume { get; set; }
        public UserRol Rol { get; set; }
        public bool IsEnable { get; set; }
    }

}
