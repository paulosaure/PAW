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
        public String atelier;
        [DataMember]
        public String bloc1;

        [DataMember]
        public String bloc2;

        [DataMember]
        public String bloc3;

        [DataMember]
        public String bloc4;

        [DataMember]
        public String bloc5;

        [DataMember]
        public String bloc6;

    }
}
