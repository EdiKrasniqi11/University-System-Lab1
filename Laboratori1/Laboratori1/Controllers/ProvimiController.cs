﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Laboratori1.Objects;

namespace Laboratori1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvimiController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ProvimiController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select * from Provimi";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("SMISAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }
        [HttpPost]
        public JsonResult Post(Provimi provimet)
        {
            string query = @"insert into Provimi values(@Provimi,@Lenda,@Studenti)";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("SMISAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Provimi", provimet.Profesori);
                    myCommand.Parameters.AddWithValue("@Lenda", provimet.Lenda);
                    myCommand.Parameters.AddWithValue("@Studenti", provimet.Studenti);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Successful Insertion");
        }

        [HttpPut]
        public JsonResult Put(Provimi provimet)
        {
            string query = @"update Provimi set Profesori = @Profesori where Lenda = @Lenda and Studenti = @Studenti";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("SMISAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Studenti", provimet.Studenti);
                    myCommand.Parameters.AddWithValue("@Lenda", provimet.Lenda);
                    myCommand.Parameters.AddWithValue("@Profesori", provimet.Profesori);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Successful Update");
        }

        [HttpDelete("{studenti}/{lenda}")]
        public JsonResult Delete(int studenti, int lenda)
        {
            string query = @"delete from Provimi where Lenda = @Lenda and Studenti = @Studenti";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("SMISAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Studenti", studenti);
                    myCommand.Parameters.AddWithValue("@Lenda", lenda);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Successful Deletion");
        }
    }
}
