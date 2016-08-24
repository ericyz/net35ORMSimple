using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleORM.Annotation {
    public class Column : System.Attribute {
        public string Name { get; set; }

        public Column(string name) {
            Name = name;
        }
    }

}
