using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using WebApplication2.Models;
using WebApplication2.ResponseDate;

namespace WebApplication2.Controllers
{
    public class RequestController : Controller
    {
        // GET: Request
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Request1(string search)
        {
            List<Requests> result;
            using (var db = new autoservice1Entities())
            {
                var param = new MySqlParameter("@search", search)
                {
                    MySqlDbType = MySqlDbType.VarChar
                };

                string sql = @"SELECT c.mark, c.model, w.desc_work, w.value , c.state_num
                FROM car c 
                INNER JOIN work w ON c.id = w.id_car 
                WHERE c.state_num LIKE @search || '%'";
                result = db.Database.SqlQuery<Requests>(sql, param).ToList();
            }

            TempData["result"] = result;

            return RedirectToAction("Result");
        }

        public ActionResult Result()
        {
            TempData.Keep();
            return View(TempData["result"]);
        }
        [HttpPost]
        public ActionResult Request2(string search)
        {
            List<Request2> result;
            using (var db = new autoservice1Entities())
            {
                var param = new MySqlParameter("@search", search)
                {
                    MySqlDbType = MySqlDbType.VarChar
                };

                string sql = @"SELECT e.FIO, e.job_position
                            FROM executor e
                            JOIN employment em
                            ON em.id_executor = e.id
                            JOIN work w
                            ON em.id_work = w.id
                            JOIN car c
                            ON w.id_car = c.id
                            JOIN client cl
                            ON c.id_client=cl.id
                            WHERE cl.FIO LIKE '%" + search + "%'";
                result = db.Database.SqlQuery<Request2>(sql, param).ToList();
            }

            TempData["result"] = result;

            return RedirectToAction("Result2");
        }

        public ActionResult Result2()
        {
            TempData.Keep();
            return View(TempData["result"]);
        }
        [HttpPost]
        public ActionResult Request3(string search)
        {
            List<Request3> result;
            using (var db = new autoservice1Entities())
            {
                var param = new MySqlParameter("@search", search)
                {
                    MySqlDbType = MySqlDbType.VarChar
                };

                string sql = @"SELECT c.FIO, c.phone
                               FROM client c
                               JOIN car ca
                               ON ca.id_client = c.id
                               JOIN work w
                               ON w.id = ca.id
                               JOIN detail_list d_l
                               ON d_l.id_work = w.id
                               JOIN detail d
                               ON d_l.id_detail = d.id
                               WHERE d.manufactured LIKE '%" + search + "%'";
                result = db.Database.SqlQuery<Request3>(sql, param).ToList();
            }

            TempData["result"] = result;

            return RedirectToAction("Result3");
        }

        public ActionResult Result3()
        {
            TempData.Keep();
            return View(TempData["result"]);
        }
        [HttpPost]
        public ActionResult Request4(string search)
        {
            List<Request4> result;
            using (var db = new autoservice1Entities())
            {
                var param = new MySqlParameter("@search", search)
                {
                    MySqlDbType = MySqlDbType.VarChar
                };

                string sql = @"SELECT c.mark, c.model, cl.FIO
                                FROM car c 
                                INNER JOIN client cl 
                                ON cl.id = c.id_client
                                INNER JOIN work w 
                                ON c.id = w.id_car
                                INNER JOIN detail_list d_l
                                ON d_l.id_work = w.id
                                GROUP BY w.id_car
                                HAVING COUNT(d_l.id_detail) = " + search;
                result = db.Database.SqlQuery<Request4>(sql, param).ToList();
            }

            TempData["result"] = result;

            return RedirectToAction("Result4");
        }

        public ActionResult Result4()
        {
            TempData.Keep();
            return View(TempData["result"]);
        }
        [HttpPost]
        public ActionResult Request5(string search, DateTimeOffset date_first, DateTimeOffset date_second)
        {
            List<Request5> result;
            using (var db = new autoservice1Entities())
            {
                var param = new[] {
                    new MySqlParameter("@date_first", date_first.ToString("yyyy-MM-dd"))
                //{
                //    MySqlDbType = MySqlDbType.Date
                //}
                    , new MySqlParameter("@date_second",date_second.ToString("yyyy-MM-dd"))
                    //{
                    //MySqlDbType = MySqlDbType.Date
                    //}


                };

                string sql = @"SELECT c.mark, c.model, c.state_num, c.color
                                FROM car c JOIN work w 
                                ON w.id_car = c.id 
                                JOIN protocol p 
                                ON p.id = w.id_protocol 
                                WHERE p.date BETWEEN @date_first AND @date_second";
                result = db.Database.SqlQuery<Request5>(sql, param).ToList();
            }

            TempData["result"] = result;

            return RedirectToAction("Result5");
        }

        public ActionResult Result5()
        {
            TempData.Keep();
            return View(TempData["result"]);
        }

    }
}