using AutoMapper;
using GreenDonut;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WhiteWebTech.API.Entities;
using WhiteWebTech.API.Models;
using WhiteWebTech.API.Services.IServices;

namespace WhiteWebTech.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicantController : ControllerBase
    {
        private readonly IGenericServices<Applicant> genericServices;

        public IMapper _mapper;
        private ResponseDTO responseDTO;

        public ApplicantController(IGenericServices<Applicant> genericServices, IMapper mapper)
        {
            this.genericServices = genericServices;
            _mapper = mapper;
            responseDTO = new ResponseDTO();
        }


        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var data = await genericServices.GetAll();
                if (data.Count != 0)
                {
                    responseDTO.Result = _mapper.Map<List<ApplicantDTO>>(data);
                }
                else { responseDTO.Message = "No Record found !"; return Ok(responseDTO); }
            }
            catch (Exception ex)
            {
                responseDTO.Message = ex.Message;
                responseDTO.IsSuccess = false;
            }
            return Ok(responseDTO);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> Get(int Id)
        {
            try
            {
                var data = await genericServices.GetById(Id);
                if (data != null)
                {
                    responseDTO.Result = _mapper.Map<ApplicantDTO>(data);
                }
                else { responseDTO.Message = "No Record found !"; return Ok(responseDTO); }
            }
            catch (Exception ex)
            {
                responseDTO.Message = ex.Message;
                responseDTO.IsSuccess = false;
            }
            return Ok(responseDTO);
        }


     

        [HttpPost]
        [Route("PostApplicant")]
        public async Task<IActionResult> PostApplicant(ApplicantDTO applicantDto)
        {
            if (applicantDto == null)
            {
                return BadRequest("Applicant data is null.");
            }

            try
            {
                // Convert DTO to DB Model
                var applicant = new Applicant
                {
                    JobId = applicantDto.JobId,
                    ApplicantName = applicantDto.ApplicantName,
                    ApplicantDescription = applicantDto.ApplicantDescription,
                    ApplicantState = applicantDto.ApplicantState,
                    CreateDate = DateTime.UtcNow,
                    filename = applicantDto.Cv.FileName
                };
                // Handle file upload
                if (applicantDto.Cv != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await applicantDto.Cv.CopyToAsync(memoryStream);
                        applicant.Cv = memoryStream.ToArray();
                        
                    }
                    var result = await genericServices.Create(applicant);
                    responseDTO.Result = result;
                    responseDTO.IsSuccess = true;
                    return Ok(responseDTO);
                }

            }
            catch (Exception ex)
            {
                responseDTO.IsSuccess = false;
                responseDTO.Message = ex.Message;
                return BadRequest();
            }
            responseDTO.IsSuccess = false;
            responseDTO.Message = "Data cant be null";
            return BadRequest();
        }



        [HttpPut]
        public async Task<IActionResult> Update(int id ,ApplicantDTO applicantDto)
        {
            if (applicantDto == null)
            {
                return BadRequest("Applicant data is null.");
            }

            try
            {
                var applicant = await genericServices.GetById(id);
                if (applicant == null)
                {
                    return NotFound();
                }

                // Update properties from DTO
                applicant.JobId = applicantDto.JobId;
                applicant.ApplicantName = applicantDto.ApplicantName;
                applicant.ApplicantDescription = applicantDto.ApplicantDescription;
                applicant.ApplicantState = applicantDto.ApplicantState;

                // Handle file upload if a new CV is provided
                if (applicantDto.Cv != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await applicantDto.Cv.CopyToAsync(memoryStream);
                        applicant.Cv = memoryStream.ToArray();
                        var result = genericServices.Update(id, applicant);
                        responseDTO.Result = result;
                        responseDTO.IsSuccess = true;
                        return Ok(responseDTO);

                    }
                }
            }
            catch (Exception)
            {

                responseDTO.Result = null;
                responseDTO.IsSuccess = false;
                return BadRequest(responseDTO);
            }
            return BadRequest(responseDTO);
        }


        [HttpGet("DownloadCv/{id}")]
        public async Task<IActionResult> DownloadCv(int id)
        {
            var applicant = await genericServices.GetById(id);
            if (applicant == null || applicant.Cv == null)
            {
                return NotFound();
            }

            return File(applicant.Cv, "application/octet-stream", applicant.filename);
        }
    }
}
