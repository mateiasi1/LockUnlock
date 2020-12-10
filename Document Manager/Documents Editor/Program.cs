using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Documents_Editor
{
    class Program
    {
        private static DocumentManager documentManager;
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            List<BaseDocument> baseDocuments = new List<BaseDocument>();
            baseDocuments.Add(new BaseDocument(1, "C:/Users/d.s.mateiasi/source/repos/DocumentsEditor/Doc1.txt"));
            baseDocuments.Add(new BaseDocument(2, "C:/Users/d.s.mateiasi/source/repos/DocumentsEditor/Doc2.txt"));
            documentManager = new DocumentManager(baseDocuments);

            Parallel.For(0, 100, async count =>
            {
                Console.WriteLine(DateTime.Now + " " + "c1 lock doc 1");
                documentManager.Write(baseDocuments[0]);
            });
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
