using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web;
using System.Web.Http;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Services;
using Umbraco.ModelsBuilder;
using Umbraco.Web;
using Umbraco.Web.WebApi;
using WebsiteDemo;
using WebsiteDemo.Models;

namespace GettingStartedWithUmbraco
{
    [Route("api/[controller]")]
    public class TeamController : UmbracoApiController
    {

        [HttpGet]
        public string Test()
        {
            return "Test";
        }

        [HttpGet]
        public HttpResponseMessage GetAllProducts()
        {

            using (var db = new DemoDbEntities())
            {
                //var p = db.Products.Find();

                var products = db.Products.Select(x => new
                {
                    ProductID = x.ProductID,
                    Name = x.Name,
                    Price = x.Price
                    //Name = x.P,
                    //Price = x.Price
                }).ToList();


                return Request.CreateResponse(HttpStatusCode.OK, products);
            };
        }


        [HttpGet]
        public HttpResponseMessage GetAllMembersFromDb()
        {

            using (var db = new DemoDbEntities())
            {
                //var p = db.Products.Find();

                var members = db.Members.Select(x => new
                {
                    Id = x.MemberID,
                    Name = x.MemberName
                    //Name = x.P,
                    //Price = x.Price
                }).ToList();


                return Request.CreateResponse(HttpStatusCode.OK, members);
            };
        }


        [HttpGet]
        public HttpResponseMessage GetAllMembersProduct()
        {

            using (var db = new DemoDbEntities())
            {
                //var p = db.Products.Find();

                var memberproduct = db.MemberProducts.Select(x => new
                {
                    Id = x.MemberID,
                    Id1 = x.MemberID,
                    Id2 = x.ProductID
                    //Name = x.P,
                    //Price = x.Price
                }).ToList();


                return Request.CreateResponse(HttpStatusCode.OK, memberproduct);
            };
        }

        [HttpGet]
        public HttpResponseMessage ProductForEachMember()
        {


            using (var db = new DemoDbEntities())
            {


                Guid id = Guid.Parse("7afa2449-3a3f-4e61-8057-b0e68afa695b");


                var products = db.MemberProducts
                    .Where(x => x.MemberID == id)
                    .Select(y => new
                    {

                        Id = y.MemberID,
                        Name = y.Member.MemberName,
                        //NumOfProduct = y.Product.Name.Length,
                        Price = y.Product.Price,
                        Products = db.MemberProducts.Where(x => x.MemberID == id)
                        .Select(x => new
                        {
                            Name = x.Product.Name
                        }).ToList()

                    }).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, products);
            };
        }

        [HttpGet]
        public HttpResponseMessage Result()
        {
            using (var db = new DemoDbEntities())
            {

                var res = (from t1 in db.MemberProducts
                           join t2 in db.Products on t1.ID equals t2.ProductID
                           join t3 in db.Members on t2.ProductID equals t3.MemberID
                           select new
                           {
                               t2.Name
                           }).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, res);
            }
        }


        [HttpGet]
        public HttpResponseMessage IDs()
        {


            using (var db = new DemoDbEntities())
            {
                var MembersIds = db.MemberProducts.Select(x => new
                {
                    Id = x.ID

                }).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, MembersIds);
            };
        }

        public HttpResponseMessage GetAllMembers()
        {
            var item = Umbraco.TypedContent(Guid.Parse("919346ae-ff0a-47ac-9ed2-193256e074cb"));

            var members = item.Children
                .Where(x => x.IsVisible())
                .Select(x => new
                {

                    MemberName = x.GetProperty("memberName").Value,
                    MemberDescription = x.GetProperty("memberDescription").Value,
                    MemberCode = x.GetProperty("memberCode").Value


                });
            return Request.CreateResponse(HttpStatusCode.OK, members);

        }


        //public HttpResponseMessage Submissions(ContactModel model)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        using (var db = new DemoDbEntities())
        //        {

        //            var from = Umbraco.TypedContent(model.EmailAddress);
        //            if (form == null)
        //            {
        //                return Request.CreateResponse(HttpStatusCode.NotFound, $"Form, {model.EmailAddress}, not found.");
        //            }
        //            var FirstName = Umbraco.TypedContent(model.FirstName);
        //            if (FirstName == null)
        //            {
        //                return Request.CreateResponse(HttpStatusCode.NotFound, $"FistName, {model.FirstName}, not found.");
        //            }
        //            var LastName = Umbraco.TypedContent(model.LastName);
        //            if (LastName == null)
        //            {
        //                return Request.CreateResponse(HttpStatusCode.NotFound, $"LastName, {model.LastName}, not found.");
        //            }
        //            var Message = Umbraco.TypedContent(model.Message);
        //            if (Message == null)
        //            {
        //                return Request.CreateResponse(HttpStatusCode.NotFound, $"Message, {model.Message}, not found.");
        //            }
        //            var newSubmission = db.Emails.Add();
                        
        //        }
                

        //    }

    }
}