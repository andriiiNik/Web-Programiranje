import{Butik} from "./Butik.js"

export class Grad
{
    constructor(id,naziv)
    {
        this.id=id;
        this.naziv=naziv;
        this.listaButika=[];
    }

    crtaj(host)
    {
       var gradKontejner=document.createElement("div");
       gradKontejner.className="gradKontejner";
       host.appendChild(gradKontejner);

       var imeGrada=document.createElement("div");
       imeGrada.className="imeGradaKont";

       let l=document.createElement("label");
       l.innerHTML=this.naziv;
       l.className="imeGrada";
       imeGrada.appendChild(l);
       gradKontejner.appendChild(imeGrada);

       var buticiKontejner=document.createElement("div");
       buticiKontejner.className="buticiKontejner";
       gradKontejner.appendChild(buticiKontejner);

       this.listaButika.forEach(butik=>{
           var b=new Butik(butik.id,butik.nazivButika,butik.telefon,butik.adresa,this.id);
           b.crtajButikSaArtiklima(buticiKontejner);
       });
    }
}