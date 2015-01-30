using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace PaintSurface
{
    [DataContract]
    class Frise
    {
        [DataMember]
        public String atelier="brossage de dents";
        [DataMember]
        public String bloc1= "image";

        [DataMember]
        public String bloc2 = "image";

        [DataMember]
        public String bloc3 = "image";

        [DataMember]
        public String bloc4 = "image";

        [DataMember]
        public String bloc5 = "image";

        [DataMember]
        public String bloc6 = "image";

    }
}
