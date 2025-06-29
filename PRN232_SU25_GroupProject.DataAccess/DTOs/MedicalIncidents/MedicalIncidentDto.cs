﻿using PRN232_SU25_GroupProject.DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN232_SU25_GroupProject.DataAccess.DTOs.MedicalIncidents
{
    public class MedicalIncidentDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentCode { get; set; }
        public int NurseId { get; set; }
        public string NurseName { get; set; }
        public IncidentType Type { get; set; }
        public string TypeDisplay => Type.ToString();
        public string Description { get; set; }
        public string Symptoms { get; set; }
        public string Treatment { get; set; }
        public IncidentSeverity Severity { get; set; }
        public string SeverityDisplay => Severity.ToString();
        public bool ParentNotified { get; set; }
        public DateTime IncidentDate { get; set; }
        public List<MedicationGivenDto> MedicationsGiven { get; set; } = new List<MedicationGivenDto>();
    }
}
