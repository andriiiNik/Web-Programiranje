import { Artikal } from "./Artikal.js";

export class Butik
{
    constructor(id,nazivButika,telefon,adresa,idGrada)
    {
        this.id=id;
        this.nazivButika=nazivButika;
        this.telefon=telefon;
        this.adresa=adresa;
        this.idGrada=idGrada;
        this.nizArtikala=[];
        this.butikKontejner=null;
    }

    crtajButikSaArtiklima(host)
    {
        this.butikKontejner=document.createElement("div");
        this.butikKontejner.className="butikKontejner";
        host.appendChild(this.butikKontejner);

        var butikHeader=document.createElement("div");
        butikHeader.className="butikHeader";
        this.butikKontejner.appendChild(butikHeader);

        this.iscrtajHeader(butikHeader);
        this.iscrtajSveArtikle(this.butikKontejner);
    }

    iscrtajHeader(host)
    {
        var butikInfo=document.createElement("div");
        butikInfo.className="butikInfo";
        host.appendChild(butikInfo);

        let d=document.createElement("div");
        d.className="divZaInfoButik";
        butikInfo.appendChild(d);
        let l=document.createElement("label");
        l.innerHTML=this.nazivButika;
        d.appendChild(l);

        d=document.createElement("div");
        d.className="divZaInfoButikAdresa";
        butikInfo.appendChild(d);
        l=document.createElement("label");
        l.className="labelaZaAdresu";
        l.innerHTML=this.adresa;
        d.appendChild(l);

        d=document.createElement("div");
        d.className="divZaInfoButikTelefon";
        butikInfo.appendChild(d);
        l=document.createElement("label");
        l.className="labelaZaTelefon";
        l.innerHTML='0'+this.telefon;
        d.appendChild(l);

        d=document.createElement("div");
        d.className="divDugmeIzmeni";
        butikInfo.appendChild(d);
        let dugme=document.createElement("button");
        dugme.innerHTML="Izmeni";
        dugme.className="dugmeIzmeni";
        dugme.onclick=(ev)=>{this.izmeniButik();}
        d.appendChild(dugme);

    }
    izmeniButik()
    {
        var divAdresa=this.butikKontejner.querySelector(".divZaInfoButikAdresa");
        let l=this.butikKontejner.querySelector(".labelaZaAdresu");
        divAdresa.removeChild(l);

        var inputAdresa=document.createElement("input");
        inputAdresa.className="inputAdresa";
        inputAdresa.placeholder=this.adresa;
        divAdresa.appendChild(inputAdresa);

        var divTelefon=this.butikKontejner.querySelector(".divZaInfoButikTelefon");
        l=this.butikKontejner.querySelector(".labelaZaTelefon");
        divTelefon.removeChild(l);

        var inputTelefon=document.createElement("input");
        inputTelefon.className="inputTelefon";
        inputTelefon.placeholder='0'+this.telefon;
        divTelefon.appendChild(inputTelefon);

        var dIzmeni=this.butikKontejner.querySelector(".dugmeIzmeni"); 
        var roditelj=dIzmeni.parentNode;
        roditelj.removeChild(dIzmeni);

        var dugmeSacuvajIzmene=document.createElement("button");
        dugmeSacuvajIzmene.className="dugmeZaCuvanjeIzmena";
        dugmeSacuvajIzmene.innerHTML="Sačuvaj izmene";
        roditelj.appendChild(dugmeSacuvajIzmene);

        var dugmeOdustani=document.createElement("button");
        dugmeOdustani.className="dugmeOdustani";
        dugmeOdustani.innerHTML="Odustani";
        roditelj.appendChild(dugmeOdustani);

        dugmeSacuvajIzmene.onclick=(ev)=>{this.sacuvajIzmene(inputAdresa,inputTelefon);}
        dugmeOdustani.onclick=(ev)=>{this.odustani();}
    }

    odustani()
    {
        this.iscrtajButikInfo();
    }

    iscrtajButikInfo()
    {
        var butikInfo=this.butikKontejner.querySelector(".butikInfo");
        var butikH=butikInfo.parentNode;
        butikH.removeChild(butikInfo);

        this.iscrtajHeader(butikH);
    }

    sacuvajIzmene(inputAdresa,inputTelefon)
    {
        this.adresa=inputAdresa.value;
        this.telefon=inputTelefon.value;
        fetch("https://localhost:5001/Butik/IzmeniButik/"+this.id+"/"+this.idGrada+"/"+this.telefon+"/"+this.adresa,
        {
            method:"PUT"
        }).then(p=>{
            if(p.ok)
            {
                this.iscrtajButikInfo();
            }
        })
    }

    iscrtajSveArtikle(host)
    {
        fetch("https://localhost:5001/Butik/VratiListuArtikalaZaButik/"+this.id)
        .then(p=>{
            var artikliKontejner=document.createElement("div");
            artikliKontejner.className="artikliKontejner";
            artikliKontejner.id=this.id;
            host.appendChild(artikliKontejner);
            p.json().then(data=>{
                data.forEach(element => {
                    var a=new Artikal(element.artikal.sifra,element.artikal.brend,element.artikal.model,element.artikal.cena,element.velicina);
                    this.nizArtikala.push(a);
                });
                this.nizArtikala.forEach(a=>{
                    var artikalKontejner=document.createElement("div");
                    artikalKontejner.className="artikalKontejner";

                    artikalKontejner.id=a.sifra;

                    artikliKontejner.appendChild(artikalKontejner);
        
                    let d=document.createElement("div");
                    d.className="divZaInfo";
                    artikalKontejner.appendChild(d);
                    let l=document.createElement("label");
                    l.innerHTML=a.brend;
                    d.appendChild(l);

                    d=document.createElement("div");
                    d.className="divZaArtikal";
                    artikalKontejner.appendChild(d);
                    l=document.createElement("label");
                    l.innerHTML=a.model;
                    d.appendChild(l);

                    d=document.createElement("div");
                    d.className="divZaInfo";
                    artikalKontejner.appendChild(d);
                    l=document.createElement("label");
                    l.innerHTML=a.cena;
                    d.appendChild(l);


                    d=document.createElement("div");
                    d.className="divZaInfo";
                    artikalKontejner.appendChild(d);
                    l=document.createElement("label");
                    l.innerHTML=a.velicina;
                    d.appendChild(l);

                    d=document.createElement("div");
                    d.className="divDugmeIzbrisi";
                    artikalKontejner.appendChild(d);
                    var dugme=document.createElement("button");
                    dugme.innerHTML="Obriši";
                    dugme.onclick=(ev)=>{this.obrisiArtikal(a.sifra,this.id);}
                    d.appendChild(dugme);
                    
                });
                let d=document.createElement("div");
                d.className="divDugmeDodaj";
                artikliKontejner.appendChild(d);
                var dugme=document.createElement("button");
                dugme.innerHTML="Dodaj Artikal";
                dugme.onclick=(ev)=>{this.dodajArtikal();} 
                d.appendChild(dugme);
            })
        })
    }

    obrisiArtikal(sifraArtikla,idButika)
    {
        fetch("https://localhost:5001/Butik/IzbrisiArtikalIzButika/"+sifraArtikla+"/"+idButika,
        {
            method:"DELETE"
        }).then(p=>{
            if(p.ok)
            {
                var a=document.getElementById(sifraArtikla);
                var roditelj=a.parentNode;
                roditelj.removeChild(a);
            }
        })
    }

    ObrisiPrethodnuFormuZaArtikal()
    {
        var desnaS=document.querySelector(".desnaStrana");
        var roditeljS=desnaS.parentNode;
        roditeljS.removeChild(desnaS);
    }

    dodajArtikal()
    {
        var desnaS=document.querySelector(".desnaStrana");
        if(desnaS!==null)
        { this.ObrisiPrethodnuFormuZaArtikal();}
        
        var desniKont=document.body.querySelector(".desniKontejner");
        var desnaStrana=document.createElement("div");
        desnaStrana.className="desnaStrana";
        desniKont.appendChild(desnaStrana);

        var desnaKartica=document.createElement("div");
        desnaKartica.className="desnaKartica";
        desnaStrana.appendChild(desnaKartica);

        for(let i=0;i<4;i++)
        {
            let d=document.createElement("div");
            d.className="divZaInputEl";
            desnaKartica.appendChild(d);
        }

        var sviInputEl=document.querySelectorAll(".divZaInputEl");

        let l=document.createElement("label");
        l.innerHTML="Brend: ";
        l.className="artikalLabele";
        sviInputEl[0].appendChild(l);
        let inputEl=document.createElement("input");
        inputEl.className="brendInput";
        sviInputEl[0].appendChild(inputEl);

        l=document.createElement("label");
        l.innerHTML="Model: ";
        l.className="artikalLabele";
        sviInputEl[1].appendChild(l);
        inputEl=document.createElement("input");
        inputEl.className="modelInput";
        sviInputEl[1].appendChild(inputEl);

        l=document.createElement("label");
        l.innerHTML="Veličina: ";
        l.className="artikalLabele";
        sviInputEl[2].appendChild(l);
        
        let combo=document.createElement("select");
        sviInputEl[2].appendChild(combo);

        var listaVelicina=["XXS","XS","S","M","L","XL","XXL","2XL","3XL"];
        let op;
        listaVelicina.forEach(velicina=>{
            op=document.createElement("option");
            op.innerHTML=velicina;
            op.value=velicina;
            combo.appendChild(op);
        })

        l=document.createElement("label");
        l.innerHTML="Cena: ";
        l.className="artikalLabele";
        sviInputEl[3].appendChild(l);
        inputEl=document.createElement("input");
        inputEl.className="cenaInput";
        sviInputEl[3].appendChild(inputEl);

        var d=document.createElement("div");
        d.className="sacuvajIodustani";
        desnaKartica.appendChild(d);
        var dugmeS=document.createElement("button");
        dugmeS.innerHTML="Sačuvaj";
        dugmeS.className="sacuvajDugme";

        var dugmeO=document.createElement("button");
        dugmeO.innerHTML="Odustani";
        dugmeO.className="odustaniDugme";
        d.appendChild(dugmeS);
        d.appendChild(dugmeO);

        dugmeS.onclick=(ev)=>{this.sacuvajArtikal(this.id);}
        dugmeO.onclick=(ev)=>{this.ObrisiPrethodnuFormuZaArtikal();}
        
    }

    sacuvajArtikal(id)
    {
        var brend=document.querySelector(".brendInput").value;
        var model=document.querySelector(".modelInput").value;
        var velicinaCombo=document.querySelector("select");
        var velicina=velicinaCombo.options[velicinaCombo.selectedIndex].value;
        var cena=parseInt(document.querySelector(".cenaInput").value);

        fetch("https://localhost:5001/Butik/DodajArtikalUButik/"+brend+"/"+model+"/"+cena+"/"+velicina+"/"+id,
        {
            method:"POST"
        }).then(p=>{
            if(p.ok)
            {
                var artikliKont=document.getElementById(id);
                var roditelj=artikliKont.parentNode;
                roditelj.removeChild(artikliKont);
                this.nizArtikala=[];
                this.iscrtajSveArtikle(roditelj);
            }
        })
    }       
}