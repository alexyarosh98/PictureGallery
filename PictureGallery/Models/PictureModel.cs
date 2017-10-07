using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PictureGallery.Models
{
    public class PictureModel
    {
    public byte[] Content { get; set; }
          public string FilePath { get; set; }    
        public string Description { get; set; }
    }
}