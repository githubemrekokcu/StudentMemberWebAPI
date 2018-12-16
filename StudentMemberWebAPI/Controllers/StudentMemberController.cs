using StudentMemberWebAPI.Models;
using StudentMemberWebAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace StudentMemberWebAPI.Controllers
{
    public class StudentMemberController : IHttpController
    {
        public async Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken)
        {
            HttpResponseMessage retVal = null; 
            if (controllerContext.Request.Method == HttpMethod.Get) // if Get Request Method
            {
                retVal = HttpGet(controllerContext);
            }
            else if (controllerContext.Request.Method == HttpMethod.Post) // if Post Request Method
            {
                retVal = await HttpPost(controllerContext);
            }
            else if (controllerContext.Request.Method == HttpMethod.Put) // if Put Request Method
            {
                retVal = await HttpPut(controllerContext);
            }
            else if (controllerContext.Request.Method == HttpMethod.Delete) // if Delete Request Method
            {
                retVal = HttpDelete(controllerContext);
            }

            return retVal;
        }

        private HttpResponseMessage HttpDelete(HttpControllerContext controllerContext) //Delete Request Method
        {
            HttpResponseMessage retVal = null;

            var id = controllerContext.RouteData.Values["id"]; // Gelen Get İsteğinde Gelen Parametreyi Alıyoruz.
            if (id == null)
            {
                retVal = controllerContext.Request.
                        CreateErrorResponse(HttpStatusCode.BadRequest, "Cannot call this metod withot Id."); ;
            }
            else
            {
                int idAsInteger;
                if (!int.TryParse(id.ToString(), out idAsInteger))
                {
                    retVal = controllerContext.Request.
                        CreateErrorResponse(HttpStatusCode.BadRequest, "Id must be a numaric value.");
                }
                else
                {
                    if (StudentMemberRepositories.IsExist(idAsInteger))
                    {
                        retVal = controllerContext.Request.
                        CreateErrorResponse(HttpStatusCode.BadRequest, "Unable to find Student Member with given Id.");
                    }
                    else
                    {
                        StudentMemberRepositories.Remove(idAsInteger);
                        retVal = controllerContext.Request.
                            CreateResponse(HttpStatusCode.OK);
                    }
                }
            }

            return retVal;
        }

        private async Task<HttpResponseMessage> HttpPut(HttpControllerContext controllerContext) // if Put Request Method
        {
            HttpResponseMessage retVal = null;
            string contentAsString = await controllerContext.Request.Content.ReadAsStringAsync();
            StudentMember postedStudentMember = await Newtonsoft.Json.JsonConvert.DeserializeObjectAsync<StudentMember>(contentAsString);
            if (StudentMemberRepositories.IsExist(postedStudentMember.FullName))
            {
                retVal = controllerContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Unable to find Student Member with given Id");
            }
            else
            {
                StudentMemberRepositories.Update(postedStudentMember);
                retVal = controllerContext.Request.CreateResponse(HttpStatusCode.OK, postedStudentMember);
            }
            return retVal;
        }

        private async Task<HttpResponseMessage> HttpPost(HttpControllerContext controllerContext)// if Post Request Method
        {
            HttpResponseMessage retVal = null;
            string contentAsString = await controllerContext.Request.Content.ReadAsStringAsync();
            StudentMember postedStudentMember = await Newtonsoft.Json.JsonConvert.DeserializeObjectAsync<StudentMember>(contentAsString);
            if (StudentMemberRepositories.IsExist(postedStudentMember.FullName))
            {
                retVal = controllerContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Another Studen member the same Fullnam is already exist.");
            }
            else
            {
                StudentMember studentMember = StudentMemberRepositories.Add(postedStudentMember.FullName, postedStudentMember.Age, postedStudentMember.Section
                    , postedStudentMember.ClassNumber);
                retVal = controllerContext.Request.CreateResponse(HttpStatusCode.OK, studentMember);
            }
            return retVal;

        }

        private HttpResponseMessage HttpGet(HttpControllerContext controllerContext)// if Get Request Method
        {
            HttpResponseMessage retVal = null;

            var id = controllerContext.RouteData.Values["id"]; // Gelen Get İsteğinde Gelen Parametreyi Alıyoruz.
            if (id == null)
            {
                var content = StudentMemberRepositories.Get();
                retVal = controllerContext.Request.
                    CreateResponse(HttpStatusCode.OK,content);
            }
            else
            {
                int idAsInteger;
                if (!int.TryParse(id.ToString(), out idAsInteger))
                {
                    retVal = controllerContext.Request.
                        CreateErrorResponse(HttpStatusCode.BadRequest, "Id must be a numaric value.");
                }
                else
                {
                    var content = StudentMemberRepositories.Get(idAsInteger);
                    retVal = controllerContext.Request.
                        CreateResponse(HttpStatusCode.OK, content);
                }
            }

            return retVal;
        }
        
    }
}