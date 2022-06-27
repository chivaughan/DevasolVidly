using AutoMapper;
using DevasolVidly.Dtos;
using DevasolVidly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

namespace DevasolVidly.Controllers.Api
{
    //[Authorize(Roles = RoleName.CanManageMovies)]
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET api/movies
        public IHttpActionResult GetMovies(string query = null)
        {
            var moviesQuery = _context.Movies.Include(c => c.Genre).Where(m => m.RentalQuantityAvailable >0);

            if (!String.IsNullOrWhiteSpace(query))
                moviesQuery = moviesQuery.Where(c => c.Name.Contains(query));

            var moviesDto = moviesQuery.ToList().Select(Mapper.Map<Movie, MovieDto>);
            return Ok(moviesDto);
        }

        //GET api/movies/id
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);
            if (movie == null)
                return NotFound();
            else
            {
                return Ok(Mapper.Map<Movie, MovieDto>(movie));
            }
        }

        // POST api/movies
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var movie = Mapper.Map<MovieDto, Movie>(movieDto);
                _context.Movies.Add(movie);
                _context.SaveChanges();

                movieDto.Id = movie.Id;
                return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
            }
        }

        // PUT api/movies/id
        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var movieInDb = _context.Movies.SingleOrDefault(c => c.Id == id);
                if (movieInDb == null)
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                else
                {
                    Mapper.Map(movieDto, movieInDb);

                    _context.SaveChanges();
                    return Ok();
                }
            }
        }

        // DELETE api/movies/id
        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(c => c.Id == id);
            if (movieInDb == null)
                return NotFound();
            else
            {
                _context.Movies.Remove(movieInDb);

                _context.SaveChanges();
                return Ok();
            }
        }
    }
}
