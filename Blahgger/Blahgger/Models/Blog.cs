using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blahgger.Models
{
    public class Blog
    {
        public int Id { get; set; }

        public int UsersId { get; internal set; }

        public string Text { get; set; }

        public DateTimeOffset CreatedOn { get; internal set; }
    }
}