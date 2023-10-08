# Generador de PDF

Proyecto webApi de cuatro capas usando NetCore7.0 para generar informes en archivos PDF del boletín final de entrega de notas, haciendo uso de la biblioteca DinkToPdf y Microsoft.AspNet.Mvc,  para los estudiantes del colegio La Estrellita de Bucaramanga empleando como gestor de base de datos MySQL. 

### ¿Qué se va obtener?
  * Informe en PDF del boletín de notas final de un estudiante.
  * Informe en PDF del boletín de notas de cada uno de los estudiantes.
  * Informe en PDF de los 3 primeros estudiantes con mayor promedio de las notas por materia.

### Pre-requisitos 📋
MySQL<br>
NetCore 7.0
### Base de datos
![image](https://github.com/yllensc/generatePDFCsharp/assets/131481951/9afbcdb0-d825-4b54-993e-a9b73b610393)

### Ejecutar proyecto 🔧
1. Clone el repositorio en la carpeta que desee abriendo la terminal y ejecute el siguiente code
   ```
   git clone https://github.com/yllensc/generatePDFCsharp.git
   ```
2. Acceda al la carpeta que se acaba de generar
   ```cd generatePDFCsharp ```
3. Ahora ejecute el comando ```. code``` para abrir el proyecto en Visual Studio Code
4. En la carpeta API diríjase al archivo appsettings.Development.json
     Llene los campos según sea su caso en los valores server, user y password reemplazando las comillas simples.

     <b>Nota:</b> Puede cambiar el nombre de la base de datos (database) si así lo prefiere.
    ![image](https://github.com/yllensc/generatePDFCsharp/assets/131481951/43fab9f9-3a30-4ca3-a3fc-9762b0075b90)
5. Ahora abra una nueva terminal en Visual Studio Code
   ![image](https://github.com/yllensc/generatePDFCsharp/assets/131481951/4a675c15-3c57-47b1-af7b-67b4c18ef3b7)
6. Ejecute las siguientes líneas de código para migrar la Base de Datos a su servidor. <br>
     ```dotnet ef migrations add FirstMigration --project ./Persistence/ --startup-project ./API/ --output-dir ./Data/Migrations ```<br><br>
     ```dotnet ef database update --project ./Persistence --startup-project ./API```
8. Accede a la carpeta API ```cd API ``` y ejecuta el comando    ```dotnet run ```<br>
  Le aparecerá algo como esto:<br>
  ![image](https://github.com/yllensc/generatePDFCsharp/assets/131481951/9eede092-b899-4c32-9c14-a78721f05b2b)
<br>Nota:<br> Tenga en cuenta que el servidor es local y el puerto puede cambiar. <br>

¡Listo! Ahora podrá ejecutar los endpoints sin problema.<br>

## Ejecutando los Endpoints ⚙️📚
1. Informe en PDF del boletín de notas final de un estudiante<br>
   <b>Nota:</b> Reemplazar {id} por el id del estudiante.
   * ```http://localhost:5062/generate-reportStudent/{id} ```<br>
3. Informe en PDF del boletín de notas de cada uno de los estudiantes<br>
    * ```http://localhost:5062/generate-reportStudents ```<br>
4. Informe en PDF de los 3 primeros estudiantes con mayor promedio de las notas por materia<br>
    * ```http://localhost:5062/generate-best-averages```<br>

## Autores ✒️

* **Margie Bocanegra** - [Marsh1100](https://github.com/Marsh1100)
* **Yllen Santamaría** - [Yllensc](https://github.com/yllensc)
    





    


