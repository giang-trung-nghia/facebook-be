using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Application.Dtos.Base
{
    public class BaseDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người sửa lần cuối
        /// </summary>
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// Thời gian sửa lần cuối
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
    }
}
