using ButiciBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ButiciBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ButikController:ControllerBase
    {
        public ButiciContext _dbContext { get; set; }
        private static List<string> listaVelicina = new List<string>() {"XXS","XS","S","M","L","XL","XXL","2XL","3XL"}; 
        public ButikController(ButiciContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Route("VratiListuGradovaiButika")]
        [HttpGet]
        public async Task<ActionResult> VratiListuGradovaiButika()
        {
            try
            {
                var listaGradova= await _dbContext.Gradovi
                .Include (a=>a.GradAdrese)
                .ThenInclude(b=>b.Butik)
                .ToListAsync();

                return Ok(listaGradova);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("VratiListuArtikalaZaButik/{idButika}")]
        [HttpGet]
        public async Task<ActionResult> VratiListuArtikalaZaButik(int idButika)
        {
            try
            {
                var butik=await _dbContext.Butici.FindAsync(idButika);
                var listaA=await _dbContext.Velicine.Where(p=>p.Butik==butik)
                .Include(c=>c.Artikal)
                .ToListAsync();

                return Ok(listaA);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("DodajArtikalUButik/{brend}/{model}/{cena}/{velicina}/{idButika}")]
        [HttpPost]
        public async Task<ActionResult> DodajArtikalUButik(string brend,string model,int cena,string velicina,int idButika)
        {
            if(brend.Length>100)
            {
                return BadRequest("Prosleđeni brend je duži od dozvoljenog. Maksimalan broj karaktera je 100");
            }
            if(model.Length>150)
            {
                return BadRequest("Prosleđeni model je duži od dozvoljenog. Maksimalan broj karaktera je 150");
            }
            if(cena<=0)
            {
                return BadRequest("Prosleđena cena mora biti veća od 0");
            }
            if(velicina.Length>50 && !listaVelicina.Contains(velicina.ToUpper()))
            {
                return BadRequest("Prosleđena veličina nije validna");
            }
              
            try
            {
                List<Artikal> listaArtikala= await _dbContext.Artikli.ToListAsync();
                int sifra=0;
                listaArtikala.ForEach(p=>{
                    if(p.Brend==brend &&p.Cena==cena && p.Model==model)
                        {
                            sifra=p.Sifra;
                        }
                });

                Artikal a=new Artikal
                {
                    Brend=brend,
                    Model=model,
                    Cena=cena
                };
               
                if(sifra==0)
                {
                    _dbContext.Artikli.Add(a);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    a=await _dbContext.Artikli.FindAsync(sifra);
                }

                var nadjenButik=await _dbContext.Butici.FindAsync(idButika);
                SpojVelicina sv=new SpojVelicina
                {
                    Velicina=velicina,
                    Butik=nadjenButik,
                    Artikal=a
                };
                _dbContext.Velicine.Add(sv);

                await _dbContext.SaveChangesAsync();

                return Ok("Artikal je uspešno dodat!");        
            }
            catch(Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        [Route("IzmeniButik/{idButika}/{idGrada}/{telefon}/{adresa}")]
        [HttpPut]
        public async Task<ActionResult> IzmeniButik(int idButika,int idGrada,int telefon,string adresa)
        {
            if(idButika<=0)
            {
                return BadRequest("Ne postoji butik sa prosleđenim ID-jem. ID mora biti veći od 0");
            }
            if(idGrada<=0)
            {
                return BadRequest("Ne postoji grad sa prosleđenim ID-jem!.ID mora biti veći od 0");
            }
            if(telefon.ToString().Length>50)
            {
                return BadRequest("Broj telefona je duzi od dozvoljenog");
            }
            if(adresa.Length>200)
            {
                return BadRequest("Prosleđena adresa je duža od dozvoljene");
            }
            try
            {
                var butikZaIzmenu=await _dbContext.Butici.FindAsync(idButika);
                butikZaIzmenu.Telefon=telefon;
                _dbContext.Update(butikZaIzmenu);
                await _dbContext.SaveChangesAsync();
                
                var nadjenGrad= await _dbContext.Gradovi.FindAsync(idGrada);

                List<SpojAdresa> listaAdresa=await _dbContext.Adrese.Where(p=>p.Butik==butikZaIzmenu).ToListAsync<SpojAdresa>();
                var spoj=listaAdresa.Where(q=>q.Grad==nadjenGrad).FirstOrDefault();
                spoj.Adresa=adresa;

                _dbContext.Update(spoj);
                await _dbContext.SaveChangesAsync();

                return Ok("Butik je uspešno izmenjen!");
            }
            catch(Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        [Route("IzbrisiArtikalIzButika/{sifra}/{idButika}")]
        [HttpDelete]
        public async Task<ActionResult> IzbrisiArtikalIzButika(int sifra,int idButika)
        {
            if(sifra<=0)
            {
                return BadRequest("Ne postoji artikal sa prosleđenom šifrom. Šifra mora biti veća od 0");
            }
            if(idButika<=0)
            {
                return BadRequest("Ne postoji butik sa prosleđenim ID-jem. ID mora biti veća od 0");
            }
            try
            {
                var artikal=await _dbContext.Artikli.FindAsync(sifra);
                List<SpojVelicina> listaVelicina=await _dbContext.Velicine.Where(p=>p.Artikal==artikal).ToListAsync<SpojVelicina>();
                var butik=await _dbContext.Butici.FindAsync(idButika);
                var butikArtikal=listaVelicina.Where(q=>q.Butik==butik).FirstOrDefault();

                _dbContext.Velicine.Remove(butikArtikal);
                await _dbContext.SaveChangesAsync();
                
                listaVelicina=await _dbContext.Velicine.Where(p=>p.Artikal==artikal).ToListAsync<SpojVelicina>();
                int nadjen=0;
                listaVelicina.ForEach(p=>{
                    if(p!=null)
                    {
                        nadjen=1;
                    }
                });

                if(nadjen==0)
                {
                    var art=await _dbContext.Artikli.FindAsync(sifra);
                     _dbContext.Artikli.Remove(artikal);

                }
    
                await _dbContext.SaveChangesAsync();
                return Ok($"Uspešno izbrisan artikal iz butika sa nazivom :{butik.NazivButika}");
            }
            catch(Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }
     


        //Izbrisi artikal (artikal se brise iz svih butika koji ga poseduju)
        /*[Route("IzbrisiArtikal/{sifra}")]
        [HttpDelete]
        public async Task<ActionResult> IzbrisiArtikal(int sifra)
        {
            if(sifra<=0)
            {
                return BadRequest("Ne postoji artikal sa prosleđenom šifrom. Šifra mora biti veća od 0");
            }
            try
            {
                var artikal=await _dbContext.Artikli.FindAsync(sifra);
                if(artikal!=null)
                {
                    List<SpojVelicina> listaArtikala=await _dbContext.Velicine.Where(p=>p.Artikal==artikal).ToListAsync<SpojVelicina>();
                    foreach(SpojVelicina sp in listaArtikala)
                    {
                        _dbContext.Velicine.Remove(sp);
                        await _dbContext.SaveChangesAsync();
                    }

                    _dbContext.Artikli.Remove(artikal);
                    await _dbContext.SaveChangesAsync();

                    return Ok("Artikal je povučen iz svih butika!");
                }
                else
                {
                    return StatusCode(403, new { error = "Zadati artikal ne postoji" });
                }
                
            }
            catch(Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }
        //Dodavanje Butika sa Adresom u odredjenom Gradu
        [Route("DodajButikUGrad/{idGrada}/{naziv}/{telefon}/{adresa}")]
        [HttpPost]
        public async Task<ActionResult> DodajButikUGrad(int idGrada,string naziv,int telefon,string adresa)
        {
            if(idGrada<=0)
            {
                return BadRequest("Ne postoji grad sa prosleđenim ID-jem. ID mora biti veći od 0");
            }
            if(naziv.Length>150)
            {
                return BadRequest("Naziv grada je duži od dozvoljenog");
            }
            if(telefon.toString().Length>255)
            {
                return BadRequest("Broj telefona je duzi od dozvoljenog");
            }
            if(adresa.Length>200)
            {
                return BadRequest("Prosleđena adresa je duža od dozvoljene");
            }
            try
            {
                Butik b=new Butik
                {
                    NazivButika=naziv,
                    Telefon=telefon,
                };
                _dbContext.Butici.Add(b);

                var nadjenGrad=await _dbContext.Gradovi.FindAsync(idGrada); 
                SpojAdresa sp=new SpojAdresa
                {
                    Adresa=adresa,
                    Butik=b,
                    Grad=nadjenGrad
                };

                _dbContext.Adrese.Add(sp);
                await _dbContext.SaveChangesAsync();

                return Ok($"Butik je uspešno dodat u grad :{nadjenGrad.NazivGrada}");

            }
            catch(Exception e)
            {
                return BadRequest(e.InnerException.Message);
            }
        }

        
        [Route("IzmeniArtikal")]
        [HttpPut]
        public async Task<ActionResult> IzmeniArtikal([FromBody] Artikal artikal)
        {
            if(artikal.Sifra<=0)
            {
                return BadRequest("Ne postoji artikal sa prosleđenom šifrom. Šifra mora biti veća od 0");
            }
            if(artikal.Brend.Length>100)
            {
                return BadRequest("Prosleđeni brend je duži od dozvoljenog. Maksimalan broj karaktera je 100");
            }
            if(artikal.Model.Length>150)
            {
                return BadRequest("Prosleđeni model je duži od dozvoljenog. Maksimalan broj karaktera je 150");
            }
            if(artikal.Cena<=0)
            {
                return BadRequest("Prosleđena cena mora biti veća od 0");
            }
            try
            {
                var izmenjenArtikal= await _dbContext.Artikli.FindAsync(artikal.Sifra);
                izmenjenArtikal.Brend=artikal.Brend;
                izmenjenArtikal.Model=artikal.Model;
                izmenjenArtikal.Cena=artikal.Cena;

                _dbContext.Artikli.Update(izmenjenArtikal);
                 await  _dbContext.SaveChangesAsync();

                 return Ok($"Artikal sa šifrom: {izmenjenArtikal.Sifra} je uspešno izmenjen!");

            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }*/
    }
}
