using System;

namespace CyProject.Model
{
    public class Task2Model
    {

        public string Id { get; set; }  //系统内唯一的id
        
        public string InFastqPath { get; set; }

        public string InRefFaPath { get; set; }


        public bool IsComplete = false;

        public string OutSnvPath { get; set; }

        public string OutSnpPath { get; set; }
    }
}
