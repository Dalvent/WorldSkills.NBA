namespace NBAManagement.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Drawing.Imaging;
    using System.IO;

    public partial class Pictures
    {
        public int Id { get; set; }

        [Required]
        public byte[] Img { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public int NumberOfLike { get; set; }

        public DateTime CreateTime { get; set; }

        public void SaveAsFile(string filePath, ImageFormat format)
        {
            using(var ms = new MemoryStream(Img))
            {
                var image = System.Drawing.Image.FromStream(ms);
                image.Save(filePath, format);
            }
        }
    }
}
