using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace OLA.Data.Models
{
	public class BaseEntity
	{
		[Key]
		public Guid Id { get; set; }
		public DateTime Modified { get; set; }
		public Guid ModifiedBy { get; set; }
		[Required]
		public bool Active { get; set; }
	}
}

