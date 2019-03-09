﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Absoft.Data.Entities
{
    [Table("ChucNang")]
    public class ChucNang
    {
        [Key]
        public string MaChucNang { get; set; }
        public string TenChucNang { get; set; }
        public string DuongDan { get; set; }
        public string BieuTuong { get; set; }
        public int ViTri { get; set; }
        public bool HasRead { get; set; }
        public bool HasCreate { get; set; }
        public bool HasUpdate { get; set; }
        public bool HasDelete { get; set; }
        public bool Status { get; set; }

        public ICollection<PhanQuyen> PhanQuyens { get; set; }
    }
}
