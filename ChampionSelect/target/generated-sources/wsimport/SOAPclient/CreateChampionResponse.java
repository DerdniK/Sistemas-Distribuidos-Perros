
package SOAPclient;

import jakarta.xml.bind.annotation.XmlAccessType;
import jakarta.xml.bind.annotation.XmlAccessorType;
import jakarta.xml.bind.annotation.XmlElement;
import jakarta.xml.bind.annotation.XmlRootElement;
import jakarta.xml.bind.annotation.XmlType;


/**
 * <p>Clase Java para anonymous complex type.
 * 
 * <p>El siguiente fragmento de esquema especifica el contenido que se espera que haya en esta clase.
 * 
 * <pre>{@code
 * <complexType>
 *   <complexContent>
 *     <restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       <sequence>
 *         <element name="champion" type="{http://champs.com/lol_champs_api}champion"/>
 *       </sequence>
 *     </restriction>
 *   </complexContent>
 * </complexType>
 * }</pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "", propOrder = {
    "champion"
})
@XmlRootElement(name = "createChampionResponse")
public class CreateChampionResponse {

    @XmlElement(required = true)
    protected Champion champion;

    /**
     * Obtiene el valor de la propiedad champion.
     * 
     * @return
     *     possible object is
     *     {@link Champion }
     *     
     */
    public Champion getChampion() {
        return champion;
    }

    /**
     * Define el valor de la propiedad champion.
     * 
     * @param value
     *     allowed object is
     *     {@link Champion }
     *     
     */
    public void setChampion(Champion value) {
        this.champion = value;
    }

}
