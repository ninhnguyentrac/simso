using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimSoDep.Models
{
    public class SessionManagerModel
    {
        // Gets the current session.
        public static ThongKeSimModel ThongKeSimSession
        {
            get
            {
                var session =
                  (ThongKeSimModel)HttpContext.Current.Session["thongkesim"];
                if (session == null)
                {
                    session = new ThongKeSimModel();
                    HttpContext.Current.Session["thongkesim"] = session;
                }
                return session;
            }
            set { HttpContext.Current.Session["thongkesim"] = value; }
        }
    }
}