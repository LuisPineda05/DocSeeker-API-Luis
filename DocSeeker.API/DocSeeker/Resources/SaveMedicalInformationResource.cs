﻿namespace DocSeeker.API.DocSeeker.Resources
{
    public class SaveMedicalInformationResource
    {
        public double height { get; set; }
        public double weight { get; set; }
        public double bodyMass { get; set; }
        public string allergy { get; set; }
        public string pathological { get; set; }
        public int IdPatient { get; set; }
    }
}
