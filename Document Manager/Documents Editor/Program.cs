using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Documents_Editor
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            List<BaseDocument> baseDocuments = new List<BaseDocument>();
            baseDocuments.Add(new BaseDocument(1, "C:/Users/d.s.mateiasi/source/repos/DocumentsEditor/Doc1.txt"));
            baseDocuments.Add(new BaseDocument(2, "C:/Users/d.s.mateiasi/source/repos/DocumentsEditor/Doc2.txt"));
            DocumentManager documentManager = new DocumentManager(baseDocuments);
            
            //documentManager.Check();
            Parallel.For(0, 5, async count =>
            {
                Console.WriteLine(DateTime.Now + " " + "c1 lock doc 1");
                await documentManager.WriteAsync(baseDocuments[0]);
            });
            Console.WriteLine(DateTime.Now + " " + "c1 unlock doc 1");
            await documentManager.FinishWrite(baseDocuments[0]);
            Console.WriteLine(DateTime.Now + " " + "c1 unlock doc 1");
            await documentManager.FinishWrite(baseDocuments[0]);
            Console.WriteLine(DateTime.Now + " " + "c1 lock doc 2");
            await documentManager.WriteAsync(baseDocuments[1]);
            Console.WriteLine(DateTime.Now + " " + "c1 unlock doc 2");
            await documentManager.FinishWrite(baseDocuments[1]);
            Console.WriteLine(DateTime.Now + " " + "c1 lock doc 2");
            await documentManager.WriteAsync(baseDocuments[1]);
            Console.ReadLine();
        }
    }
}
