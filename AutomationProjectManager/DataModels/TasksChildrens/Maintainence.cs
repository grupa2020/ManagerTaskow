using AutomationProjectManager.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationProjectManager.DataModels.TasksChildrens
{
    public class Maintainence
    {
        public DateTime date;
        public string Description;
        public string People;
        public List<string> Materials;


        public Maintainence(string content)
        {
            if (content != null && content != string.Empty)
            {
                Maintainence newObject = new Maintainence();
                newObject = JsonConvert.DeserializeObject<Maintainence>(content);
                this.date = newObject.date;
                this.Description = newObject.Description;
                this.Materials = newObject.Materials;
                this.People = newObject.People;
            }
            else
            {

                this.date = DateTime.Now;
                this.Description = "Opis";
                this.Materials = new List<string>();
                this.Materials.Add("Przykładowa pozycja");
                this.People = "Uczestniczący pracownicy";
            }


        }

        public Maintainence()
        {
            //  this.BoardId = -1;
            // this.TaskId = -1;
            // this.TaskType = TaskTypeEnum.Maintainence;

        }

        public Maintainence(int BoardId, string Content, int TaskId)
        {
            //  this.BoardId = BoardId;
            //  this.TaskId = TaskId;
            // this.Content = Content;
            // this.TaskType = TaskTypeEnum.Maintainence;
        }

    }
}