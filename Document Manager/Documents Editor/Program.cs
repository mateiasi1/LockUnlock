using System;
using System.Collections.Generic;

namespace Documents_Editor
{
    class Program
    {
        static void Main(string[] args)
        {
            List<BaseDocument> baseDocuments = new List<BaseDocument>();
            baseDocuments.Add(new BaseDocument(1, "C:/Users/d.s.mateiasi/source/repos/DocumentsEditor/Doc1.txt"));
            baseDocuments.Add(new BaseDocument(2, "C:/Users/d.s.mateiasi/source/repos/DocumentsEditor/Doc2.txt"));
            DocumentManager documentManager = new DocumentManager(baseDocuments);

            //documentManager.Check();

            Console.WriteLine(DateTime.Now + " " + "c1 lock doc 1");
            documentManager.Write(baseDocuments[0]);
            Console.WriteLine(DateTime.Now + " " + "c1 lock doc 1");
            documentManager.Write(baseDocuments[0]);
            Console.WriteLine(DateTime.Now + " " + "c1 unlock doc 1");
            documentManager.FinishWrite(baseDocuments[0]);
            Console.WriteLine(DateTime.Now + " " + "c1 unlock doc 1");
            documentManager.FinishWrite(baseDocuments[0]);
            Console.WriteLine(DateTime.Now + " " + "c1 lock doc 2");
            documentManager.Write(baseDocuments[1]);
            Console.WriteLine(DateTime.Now + " " + "c1 unlock doc 2");
            documentManager.FinishWrite(baseDocuments[1]);
            Console.WriteLine(DateTime.Now + " " + "c1 lock doc 2");
            documentManager.Write(baseDocuments[1]);
            Console.ReadLine();
        }
    }
}
