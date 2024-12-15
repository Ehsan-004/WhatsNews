from .interfaces import ITagRepository, IArticleRepository, ICategoryRepository, ICommentRepository
from News.models import Category, Tag, Article, Comment
from django.db.models import Count


class CategoryRepository(ICategoryRepository):
    def get_all(self):
        return Category.objects.all()


    def get_all_with_post_count(self):
        return Category.objects.annotate(post_count=Count('article'))


    def get_by_id(self, id_):
        try:
            return Category.objects.get(pk=id_)
        except Category.DoesNotExist:
            return None


    def get_category_post_count(self, category_id):
        category = self.get_by_id(category_id)
        if category is None:
            return None
        return Article.objects.annotate(article_count=Count('article')).first()


    def create(self, **category_data : dict):
        try:
            category = Category(**category_data)
            category.save()
            return True
        except:
            return False


    def update(self, id_, category_data):
        try:
            category = Category.objects.get(pk=id_)
            for key, value in category_data.items():
                setattr(category, key, value)
            category.save()
            return True
        except Category.DoesNotExist:
            return False

    def delete(self, id_):
        try:
            category = Category.objects.get(pk=id_)
            category.delete()
            return True
        except Category.DoesNotExist:
            return False


class TagRepository(ITagRepository):

    def get_all(self):
        return Tag.objects.all()


    def get_by_id(self, id_):
        try:
            return Tag.objects.get(pk=id_)
        except Tag.DoesNotExist:
            return None


    def create(self, tag_data : dict):
        try:
            tag = Tag(**tag_data)
            tag.save()
            return True
        except:
            return False


    def update(self, id_, tag_data):
        try:
            tag = Tag.objects.get(pk=id_)
            for key, value in tag_data.items():
                setattr(tag, key, value)
            tag.save()
            return True
        except Tag.DoesNotExist:
            return False

    def delete(self, id_):
        try:
            tag = Tag.objects.get(pk=id_)
            tag.delete()
            return True
        except Tag.DoesNotExist:
            return False


class ArticleRepository(IArticleRepository):

    def get_all(self):
        return Article.objects.all()


    def get_all_names(self):
        articles = []
        article_ids = []
        all_ = Article.objects.all()
        for article in all_:
            articles.append(article.title)
            article_ids.append(article.id)
        return articles, article_ids


    def get_indexed_articles(self):
        return Article.objects.filter(first_level_index=True)


    def get_second_indexed_article(self):
        return Article.objects.filter(second_level_index=False).first()


    def get_third_indexed_article(self):
        return Article.objects.filter(third_level_index=True).first()


    def get_most_viewed_articles(self):
        return Article.objects.order_by('-view_count')[:3]


    def get_recent_articles(self):
        return Article.objects.order_by('-create_date')[:8]


    def get_articles_in_type(self, type_):
        return Article.objects.filter(type=type_).order_by('create_date')[:5]


    def get_by_id(self, id_):
        try:
            return Article.objects.get(pk=id_)
        except Article.DoesNotExist:
            print(Article.DoesNotExist)
            return None


    def get_by_tag(self, tag):
        return Article.objects.filter(tags=tag)


    def increase_view_count(self, id_):
        try:
            article = self.get_by_id(self, id_)
            article.view_count += 1
            article.save()
            return article.view_count
        except Exception as e:
            print(f"error text: {e}")
            return 0


    def get_by_category(self, category):
        return Article.objects.filter(category=category)


    def create(self, article_data : dict):
        try:
            article = Article(**article_data)
            article.save()
            return True
        except:
            return False


    def update(self, id_, article_data : dict):
        try:
            article = Article.objects.get(pk=id_)
            for key, value in article_data.items():
                setattr(article, key, value)
            article.save()
            return True
        except Article.DoesNotExist:
            return False


    def delete(self, id_):
        try:
            article = Article.objects.get(pk=id_)
            article.delete()
            return True
        except Article.DoesNotExist:
            return False


class CommentRepository(ICommentRepository):
    def get_all(self):
        return Comment.objects.all()

    def get_by_id(self, id_):
        try:
            return Comment.objects.get(pk=id_)
        except Comment.DoesNotExist:
            return None

    def get_by_post(self, post_id):
        try:
            return Comment.objects.filter(post_id=post_id).order_by('-create_date')
        except Comment.DoesNotExist:
            return None

    def create(self, comment_data):
        try:
            comment = Comment(**comment_data)
            comment.save()
            return comment.id
        except Exception as e:
            print(f"error text: {e}")
            return -1

    def update(self, id_, comment_data):
        try:
            comment = Comment.objects.get(pk=id_)
            for key, value in comment_data.items():
                setattr(comment, key, value)
            comment.save()
            return True
        except Comment.DoesNotExist:
            return False

    def delete(self, id_):
        try:
            comment = Comment.objects.get(pk=id_)
            comment.delete()
            return True
        except Comment.DoesNotExist:
            return False
