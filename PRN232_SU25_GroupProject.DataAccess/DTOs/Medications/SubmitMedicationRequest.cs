﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.DataAccess.DTOs.Medications
{
    public class SubmitMedicationRequest
    {
        [Required]
        public int StudentId { get; set; }

        [Required]
        public string MedicationName { get; set; }

        [Required]
        public string Dosage { get; set; }

        [Required]
        public string Instructions { get; set; }

        [Required]
        public TimeSpan AdministrationTime { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int ParentId { get; set; }
    }
}
