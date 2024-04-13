using AutoMapper;
using Gallery.DTO.DataTransferObjectClasses.Article;
using Gallery.DTO.DataTransferObjectClasses.Picture;
using Gallery.Infrastructrue.Repositories.Articles;
//using Gallery.Infrastructrue.Repositories.Pictures;
using Gallery.Models.Entities.ArticleEntity;
using Gallery.Services.BaseInterfaceAndClass;

namespace Gallery.Services.ServiceClasses.Articles
{
    public class ArticleService : ServiceBase<Article, ArticleDTO, int>, IArticleService
    {
        private readonly IMapper _mapper;
       
        public ArticleService(IMapper mapper, IArticleRepository repository) : base(repository)
        {
            _mapper = mapper;
        }

        public async override Task<ArticleDTO> GeneralValidation(ArticleDTO model)
        {
            return await Task.Run(() => new ArticleDTO());
        }

        public async override Task<ArticleDTO> CreateValidation(ArticleDTO model)
        {
            if (model == null)
            {
                model = new ArticleDTO();
                await model.SetError("اطلاعات ارسالی نامعتبر می باشد");
            }

            if (string.IsNullOrEmpty(model.Title))
            {
                await model.SetError("عنوان عکس الزامی می باشد");
            }

            return model;
        }
        //public override async Task<ArticleDTO> CreateValidation(ArticleDTO model)
        //{
        //    return await Task.Run(() => new ArticleDTO());
        //}



        public override async Task<ArticleDTO> UpdateValidation(ArticleDTO model)
        {
            if (model == null || model.Id == default)
            {
                model = new ArticleDTO();
                await model.SetError("Update امکان پذیر نمی باشد");
                return model;
            }
            return model;
        }


        public override async Task<ArticleDTO> DeleteValidation(int id)
        {
            return await Task.Run(() => new ArticleDTO());
        }



        public override ArticleDTO TranslateToDTO(Article entity)
        {
            return _mapper.Map<ArticleDTO>(entity);
        }

        public override Article TranslateToEntity(ArticleDTO model)
        {
            return _mapper.Map<Article>(model);
        }

        public async Task<ArticleDTO> TranslateCreateArticleToArticleDTO(CreateArticleDTO article)
        { 
            return await Task.Run(() =>
            new ArticleDTO() {Title = article.Title, Picture = article.Picture, PictureAlt = article.PictureAlt,
                PictureTitle = article.PictureTitle, Body = article.Body, ShortDescription = article.ShortDescription });
        }

        public async Task<ArticleDTO> CreateArticle(CreateArticleDTO articleDto)
        {
            var article = await CreateValidation(await TranslateCreateArticleToArticleDTO(articleDto));
            if (await article.HasError())
            {
                return article;
            }

            var result = await CreateAsync(article);
            await RepositoryOfEntity.CommitAsync();
            return result;
        }

        public async Task<ArticleDTO> UpdateArticle(UpdateArticleDTO updateArticle)
        {
            var result = new ArticleDTO();
            var data = await GetAsync(false, x => x.Id == updateArticle.Id);
            if (data == null)
            {
                await result.SetError("هیچ اطلاعاتی با مشخصات وارد شده یافت نشد");
                return result;
            }

            data.Title = updateArticle.Title;
            data.ShortDescription = updateArticle.ShortDescription;
            data.PictureAlt = updateArticle.AltPicture;

            var update = await UpdateAsync(TranslateToDTO(data));
            await RepositoryOfEntity.CommitAsync();
            return update;
        }


        public async Task<List<ArticleDTO>> GetArticlesAsList() => (await GetAllDTOs(null)).ToList();

        public async Task<ArticleDTO> DeleteArticle(DeleteArticleDTO deletearticle)
        {
            var result = new ArticleDTO();
            var dataArticle = await GetAsync(false, x => x.Id == deletearticle.Id);
            if (dataArticle == null)
            {
                await result.SetError("هیچ اطلاعاتی با مشخصات وارد شده یافت نشد");
                return result;
            }


            dataArticle.IsDeleted = deletearticle.IsDeleted;

            var delete = await UpdateAsync(TranslateToDTO(dataArticle));
            await RepositoryOfEntity.CommitAsync();
            return delete;
        }
    }
}
