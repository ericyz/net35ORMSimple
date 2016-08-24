using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleORM.Annotation {
    public class Table : System.Attribute {

        public string Name { get; set; }

        public Table(string name) {
            Name = name;
        }
    }

}
