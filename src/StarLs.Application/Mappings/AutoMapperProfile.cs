﻿using AutoMapper;
using StarLs.Application.Dto;
using StarLs.Application.Queries.Characters;
using StarLs.Application.Queries.Movies;
using StarLs.Application.Queries.Planets;
using StarLs.Application.Queries.Starships;
using StarLs.Application.Queries.Vehicles;
using StarLs.Core.Entities;

namespace StarLs.Application.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<GetCharacterQueryResponse, Character>().ReverseMap();
        CreateMap<GetCharacterByIdQueryResponse, Character>().ReverseMap();

        CreateMap<GetMovieQueryResponse, Movie>().ReverseMap();
        CreateMap<GetMovieByIdQueryResponse, Movie>().ReverseMap();

        CreateMap<GetPlanetQueryResponse, Planet>().ReverseMap();
        CreateMap<GetPlanetByIdQueryResponse, Planet>().ReverseMap();

        CreateMap<GetStarshipQueryResponse, Starship>().ReverseMap();
        CreateMap<GetStarshipByIdQueryResponse, Starship>().ReverseMap();

        CreateMap<GetVehicleQueryResponse, Vehicle>().ReverseMap();
        CreateMap<GetVehicleByIdQueryResponse, Vehicle>().ReverseMap();

        CreateMap<Character, CharacterDto>().ReverseMap();
        CreateMap<Movie, MovieDto>().ReverseMap();
        CreateMap<Planet, PlanetDto>().ReverseMap();
        CreateMap<Starship, StarshipDto>().ReverseMap();
        CreateMap<Vehicle, VehicleDto>().ReverseMap();
    }
}
