
using Linked_List;


ConnectedList<int> connectedListNumber=new ConnectedList<int>();
     
//AddFirst() - LinkedList'in başına veri ekler
connectedListNumber.AddFirst(0);
connectedListNumber.AddFirst(1);

//AddLast() - LinkedList'in sonuna veri ekler
connectedListNumber.AddLast(7);
connectedListNumber.AddLast(9);
connectedListNumber.AddLast(3);
connectedListNumber.AddLast(10);
  
//AddBefore()- ConnectedList'in herhangi bir verinin öncesine veri ekler
connectedListNumber.AddBefore(connectedListNumber.Find(9), 8);

//AddAfter()- ConnectedList'in herhangi bir veriden sonra veri ekler.
connectedListNumber.AddAfter(connectedListNumber.Find(9),4);

//RemoveFirst() - ConnectedList'in ilk elemanını siler.
connectedListNumber.RemoveFirst();

//RemoveLast() - ConnectedList'in son elemanını siler.
connectedListNumber.RemoveLast();

//Remove() - ConnectedList'in herhangi bir elemanını siler.
connectedListNumber.Remove(7);


/*
ConnectedListten herhangi bir nodea ait veri tutan ConnectedListLoop türünden değişken oluşturduk.
İlk eleman için First son eleman için Last aldık.
*/
ConnectedListLoop<int> mevcutEleman=connectedListNumber.First;
ConnectedListLoop<int> sonEleman=connectedListNumber.Last;

//İlk elemanın değeri
Console.WriteLine("İlk eleman:{0}",mevcutEleman.Value);

//Son elemanın değeri
Console.WriteLine("Son eleman:{0}",sonEleman.Value);

//İlk elemanın Next değeri
Console.WriteLine("İlk elemanın sonraki değeri {0}:",mevcutEleman.Next.Value);

//Son elemanın Previous değeri
Console.WriteLine("Son elemanın önceki değeri {0}:",sonEleman.Previous.Value);

while (mevcutEleman!=null)
{
    Console.WriteLine(mevcutEleman.Value);
    mevcutEleman=mevcutEleman.Next;               
}

foreach (var item in connectedListNumber)
{
    Console.WriteLine(item);
}  

