namespace Logger.Models.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Enums;

    public class Log
    {

        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public MessageLevel Level { get; set; }

        public string UserId { get; set; }

        public DateTime DateTimeCreation { get; set; }

    }
}