using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeanTechniquesPhotoAlbumTechnicalShowcase.Models
{
    internal class Photo
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int AlbumId { get; set; }

        public string URL { get; set; }

        public string ThumbnailUrl { get; set; }
    }
}
