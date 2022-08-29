using System;
using System.Collections.Generic;
using System.Linq;
namespace D1
{
    public class Customers
    {
        public string Name;
        public Customers(String name)
        {
            this.Name = name;
        }
        public override bool Equals(object obj)
        {
            Customers t = (Customers)obj;
            return this.Name==t.Name;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    public class OrderDetails
    {
        public String Goods
        {
            get;
            set;
        }
        public Double Money
        {
            get;
            set;
        }
        public Customers Customer;
        public OrderDetails(String goods,Double money,String customer)
        {
            this.Goods = goods;
            this.Money = money;
            this.Customer =new Customers(customer);
        }
        public override string ToString()
        {
            return "Goods: " + Goods + " Money: " + Money.ToString() + " Customers: " + Customer.Name+" ";
        }
        public override bool Equals(object obj)
        {
            OrderDetails t = (OrderDetails)obj;
            return (Goods==t.Goods)&&(Money==t.Money)&&(Customer.Name==t.Customer.Name);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class Order
    {
        public OrderDetails orderDetails;
        public int no;
        public Order()
        {
            no = 0;
            orderDetails = null;
        }
        public Order(OrderDetails t)
        {
            orderDetails = t;
            no = 0;
        }

        public override string ToString()
        {
            return "Order" + no.ToString()+" "+this.orderDetails.ToString();
        }
        public override bool Equals(object obj)
        {
            Order t = (Order) obj;
            return this.orderDetails.Equals(t.orderDetails);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }


    public class OrderService
    {
        public Order order = new Order();
        public List<Order> orderList = new List<Order>();
        bool isEmpty = true;


        //查找
        public IEnumerable<Order> getOrder(int t)
        {
            return from r in orderList where r.no==t select r;
        }
        public IEnumerable<Order> getOrder(Double t)
        {
            return from r in orderList where r.orderDetails.Money == t orderby r.orderDetails.Money descending select r;
        }
        public IEnumerable<Order> getOrder(String t)
        {
            return from r in orderList where r.orderDetails.Goods == t orderby r.orderDetails.Money descending select r;
        }
        public IEnumerable<Order> getOrder(Customers t)
        {
            return from r in orderList where r.orderDetails.Customer.Equals(t) orderby r.orderDetails.Money descending select r;
        }

        public bool AddOrder(Order t)
        {
            bool isSuc = true;
            if (isEmpty)
            {
                order.no++;
                t.no = order.no;
                orderList.Add(t);
                this.isEmpty = false;
            }
            else
            {
                for(int i = 1; i < orderList.Count(); i++)
                {
                    if (getOrder(i).First().orderDetails.Equals(t.orderDetails))
                    {
                        isSuc = false;
                    }
                }
                if (isSuc)
                {
                    order.no++;
                    t.no = order.no;
                    orderList.Add(t);
                }
            }
            return isSuc;
        }

        public bool DelOrder(Order t)
        {
            bool isSuc = false;
            int m = 0;
            for(int i = 1; i <= orderList.Count(); i++)
            {
                if (getOrder(i).Count()!=0)
                {
                    if (getOrder(i).First().orderDetails.Equals(t.orderDetails))
                    {
                        m = i;
                        isSuc = true;
                        orderList.RemoveAt(m-1);
                       Console.WriteLine("已删除 ");
                        break;
                    }
                    
                }
            }
            if (isSuc == false)
            {
                Console.WriteLine("不含此订单");
            }
            return isSuc;
        }

        public void SortOrderByMoney()
        {
            orderList.Sort((left, right) => {
                if (left.orderDetails.Money > right.orderDetails.Money)
                {
                    return 1;
                }
                else if(left.orderDetails.Money==right.orderDetails.Money)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            });
        }




    }



    internal class Program
    {
        static void Main(string[] args)
        {
            OrderService t = new OrderService();
            if(t.AddOrder(new Order(new OrderDetails("3080", 9999, "李一"))))
            {
                Console.WriteLine("已添加");
            }
            else
            {
                Console.WriteLine("已存在");
            }
            if (t.AddOrder(new Order(new OrderDetails("牙刷", 9, "王二"))))
            {
                Console.WriteLine("已添加");
            }
            else
            {
                Console.WriteLine("已存在");
            }
            if (t.AddOrder(new Order(new OrderDetails("3080", 9999, "李一"))))
            {
                Console.WriteLine("已添加");
            }
            else
            {
                Console.WriteLine("已存在");
            }
            if (t.AddOrder(new Order(new OrderDetails("飞机", 114514, "张三"))))
            {
                Console.WriteLine("已添加");
            }
            else
            {
                Console.WriteLine("已存在");
            }
            if (t.AddOrder(new Order(new OrderDetails("厚大法考", 50, "罗翔"))))
            {
                Console.WriteLine("已添加");
            }
            else
            {
                Console.WriteLine("已存在");
            }


            foreach (Order d in t.orderList) 
            { 
                Console.WriteLine(d.ToString()); 
            }
            
            
            
            Console.WriteLine("删除李一的订单与一个不存在的订单");
            if (t.DelOrder(new Order(new OrderDetails("3080", 9999, "李一"))))
            {
                Console.WriteLine("第一次删除后的订单如下");
                foreach (Order d in t.orderList)
                {
                    Console.WriteLine(d.ToString());
                }
            }

            if(t.DelOrder(new Order(new OrderDetails("3080", 9999, "88"))))
            {
                Console.WriteLine("第二次删除后的订单如下：");
                foreach (Order d in t.orderList)
                {
                    Console.WriteLine(d.ToString());
                }
            }
            else {}
            

           
            Console.WriteLine("查找飞机,含飞机的订单为：");

            foreach (Order m in t.getOrder("飞机"))
            {   
                Console.WriteLine(m);
            }

            Console.WriteLine("查找火车,含火车的订单为：");

            foreach (Order m in t.getOrder("火车"))
            {
                if (m == null) {
                    Console.WriteLine("无此项");
                }            
            }
            Console.WriteLine(" ");


            Console.WriteLine("以价格排序");
            t.SortOrderByMoney();
            foreach (Order d in t.orderList) 
            { 
                Console.WriteLine(d.ToString()); 
            }

        }
    }
}

