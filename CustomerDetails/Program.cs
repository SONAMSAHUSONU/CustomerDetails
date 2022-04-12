using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerDetails
{
    public class Menu
    {
        public int Cid;
        public string Name;
        public string Address;
        public long Mobile;
        public string Order;


        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome the Restorent");
            Console.WriteLine("1" + "Vag");
            Console.WriteLine("2" + "Non-Vag");
            Console.WriteLine("Enter  1 for VagFood and 2 for Non-Vag 3 for update the Order");

            int i = Convert.ToInt32(Console.ReadLine());

            Menu objMenu = new Menu();
            List<OrderDetails> Show=objMenu.GetOrder(i);
            foreach (var item in Show)
            {
                Console.WriteLine(item.id);
                Console.WriteLine(item.orderName);
                Console.WriteLine(item.orderPrice);
                Console.WriteLine(item.orderRateing);
            }
            Console.WriteLine("Please enter your Name");
            objMenu.Name = Console.ReadLine();
            Console.WriteLine("Please enter your Mobile Number");
            objMenu.Mobile = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine("Please enter your Address");
            objMenu.Address = Console.ReadLine();
            Console.WriteLine("Please enter your OrderName");
            objMenu.Order = Console.ReadLine();
            objMenu.SetOrder(objMenu);
            objMenu.UpdateOrder(objMenu);

            Console.ReadLine();

        }
        public List<OrderDetails> SetOrder(Menu objMenu)
        {
            List<OrderDetails> oSet = new List<OrderDetails>();

            string scn = ConfigurationManager.ConnectionStrings["scn"].ConnectionString;
            using (SqlConnection cn=new SqlConnection(scn))
            {
                cn.Open();

                using (SqlCommand cmd=new SqlCommand("SPCustomer2", cn))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Cid", objMenu.Cid);
                    cmd.Parameters.AddWithValue("@CName", objMenu.Name);
                    cmd.Parameters.AddWithValue("@CAddress", objMenu.Address);
                    cmd.Parameters.AddWithValue("@CMobile", objMenu.Mobile);
                    cmd.Parameters.AddWithValue("@COrder", objMenu.Order);
                    int status = cmd.ExecuteNonQuery();

                    if (status > 0 )
                    {
                        Console.WriteLine("Your OrderName Is Successfully");

                    }
                    else
                    {
                        Console.WriteLine("Your OrderName Is Faild");

                    }
                }
            }
                return oSet;
        }
        public List<OrderDetails> UpdateOrder(Menu objMenu)
        {
            List<OrderDetails> oUpdate = new List<OrderDetails>();

            string scn = ConfigurationManager.ConnectionStrings["scn"].ConnectionString;
            using (SqlConnection cn = new SqlConnection(scn))
            {
                cn.Open();

                using (SqlCommand cmd = new SqlCommand("SPCustomer2Update", cn))
                {

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Cid", objMenu.Cid);
                    cmd.Parameters.AddWithValue("@CName", objMenu.Name);
                    cmd.Parameters.AddWithValue("@CAddress", objMenu.Address);
                    cmd.Parameters.AddWithValue("@CMobile", objMenu.Mobile);
                    cmd.Parameters.AddWithValue("@COrder", objMenu.Order);
                    int status = cmd.ExecuteNonQuery();

                    if (status >=3 )
                    {
                        Console.WriteLine("Your OrderName Update Successfully");

                    }
                    else
                    {
                        Console.WriteLine("Sorry! We Can't Able To Update Your Order");

                    }
                }
            }
            return oUpdate;
        }

        public List<OrderDetails> GetOrder(int i)
        {
            string scn = ConfigurationManager.ConnectionStrings["scn"].ConnectionString;



            using (SqlConnection cn = new SqlConnection(scn))
            {

                try
                {
                    cn.Open();
                     List<OrderDetails> orders = new List<OrderDetails>();


                    if (i == 1)
                    {
                        using (SqlCommand cmd = new SqlCommand("sp_vegmenu", cn))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            SqlDataReader dr = cmd.ExecuteReader();


                            while (dr.Read())
                            {
                                OrderDetails orderDetails = new OrderDetails();
                                orderDetails.id = Convert.ToInt32(dr["v_id"]);
                                orderDetails.orderName = Convert.ToString(dr["v_Name"]);
                                orderDetails.orderPrice = Convert.ToString(dr["v_Price"]);
                                orderDetails.orderRateing = Convert.ToString(dr["v_Rateing"]);
                                orders.Add(orderDetails);

                            }

                        }
                    }
                    else if (i == 2)
                    {

                        using (SqlCommand cmd = new SqlCommand("sp_nonvegmenu", cn))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            SqlDataReader dr = cmd.ExecuteReader();


                            while (dr.Read())
                            {
                                OrderDetails orderDetails = new OrderDetails();
                                orderDetails.id = Convert.ToInt32(dr["nv_id"]);
                                orderDetails.orderName = Convert.ToString(dr["nv_Name"]);
                                orderDetails.orderPrice = Convert.ToString(dr["nv_Price"]);
                                orderDetails.orderRateing = Convert.ToString(dr["nv_Rateing"]);
                                orders.Add(orderDetails);

                            }
                        }

                    }
                    else if (i == 3)
                    {
                        Console.WriteLine("Enter your order id");
                        int Cid = Convert.ToInt32(Console.ReadLine());
                        

                    }

                    return orders;

                }

                catch (Exception ex)
                {
                    return null;
                }


                finally
                {
                    cn.Close();
                }

            }
        }
        public class OrderDetails
        {
            public int id { get; set; }
            public string orderName { get; set; }
            public string orderPrice { get; set; }
            public string orderRateing { get; set; }

        }
    }
}



