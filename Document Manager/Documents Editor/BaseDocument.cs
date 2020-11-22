using System;
using System.Collections.Generic;
using System.Text;

namespace Documents_Editor
{
    public class BaseDocument
    {
        private int v1;
        private string v2;

        public BaseDocument(int v1, string v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }

        public int Id { get; set; }
        public string Path { get; set; }
    }
}
