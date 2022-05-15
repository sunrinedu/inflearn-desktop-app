using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InflearnMirror
{
    public class CurriculumItem
    {
        public int id;
        public int? oid;
        public int? course_id;
        public int? video_file;
        public int? attachments_file;
        public int seq;
        public CurriculumItemType type;
        public string title;
        public string video_url;
        public string body;
        public bool preview;
        public int runtime;
        public List<object> editor_image_ids;
        public string created_at;
        public string updated_at;
        public string deleted_at;
        public string old_body;
        public bool has_mission;
        public string mission_body;
    }

    public enum CurriculumItemType
    {
        section,
        lecture,
    }
}
