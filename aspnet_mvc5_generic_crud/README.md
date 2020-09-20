Test Account: admin@aspnet.com  
Test Password: P@ssw0rd  
  
The generic crud pattern, the propose is inject the different objects to CRUD view settings.  
The demo path as below,  

Services:  
//aspnet_mvc5_generic_crud/ASPNET_MVC_5/Areas/Services/BaseCrudService.cs  
//aspnet_mvc5_generic_crud/ASPNET_MVC_5/Areas/Services/BaseRepository.cs  
//aspnet_mvc5_generic_crud/ASPNET_MVC_5/Areas/Services/UnitOfWork.cs  
  
Repositories:  
//aspnet_mvc5_generic_crud/ASPNET_MVC_5/Areas/Admin/Repositories/RolesRepository.cs  
//aspnet_mvc5_generic_crud/ASPNET_MVC_5/Areas/Admin/Repositories/MenusRepository.cs  
  
Controllers:  
//aspnet_mvc5_generic_crud/ASPNET_MVC_5/Areas/Admin/Repositories/RolesController.cs  
//aspnet_mvc5_generic_crud/ASPNET_MVC_5/Areas/Admin/Repositories/MenusController.cs  

Views:  
//aspnet_mvc5_generic_crud/ASPNET_MVC_5/Areas/Admin/Menus/...  
//aspnet_mvc5_generic_crud/ASPNET_MVC_5/Areas/Admin/Roles/...  
  
Shared_Views:  
//aspnet_mvc5_generic_crud/ASPNET_MVC_5/Shared/Crud/_Index.cshtml  
//aspnet_mvc5_generic_crud/ASPNET_MVC_5/Shared/Crud/_Create.cshtml  
//aspnet_mvc5_generic_crud/ASPNET_MVC_5/Shared/Crud/_Edit.cshtml  
//aspnet_mvc5_generic_crud/ASPNET_MVC_5/Shared/Crud/_Delete.cshtml  
//aspnet_mvc5_generic_crud/ASPNET_MVC_5/Shared/Crud/_Details.cshtml  