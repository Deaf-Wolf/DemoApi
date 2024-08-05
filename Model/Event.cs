using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoApi.Model
{
    [Table("events")]
    public class Event
    {
        [Key]
        [Column("eventid")]
        public Guid Id { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("eventdate")]
        public DateOnly EventDate { get; set; }

        [Column("starttime")]
        public TimeOnly StartTime { get; set; }

        [Column("endtime")]
        public TimeOnly EndTime { get; set; }
        
        [Column("description")]
        public string Description { get; set; }

        [Column("eventtype")]
        public string Eventtype { get; set; } // will decide event color

        [ForeignKey("users")]
        [Column("userid")]
        public Guid UserId { get; set; } // foreingKey of User Id

    }
}
