from News.models import Category, Tag, Article
from abc import ABC, abstractmethod


class ICategoryRepository(ABC):

    @abstractmethod
    def get_all(self):
        pass

    @abstractmethod
    def get_all_with_post_count(self):
        pass

    @abstractmethod
    def get_by_id(self, id_):
        pass

    @abstractmethod
    def get_category_post_count(self, category_id):
        pass

    @abstractmethod
    def create(self, category):
        pass

    @abstractmethod
    def update(self, id_, category):
        pass

    @abstractmethod
    def delete(self, id_):
        pass


class ITagRepository(ABC):

    @abstractmethod
    def get_all(self):
        pass

    @abstractmethod
    def get_by_id(self, id_):
        pass

    @abstractmethod
    def create(self, tag):
        pass

    @abstractmethod
    def update(self, id_, tag):
        pass

    @abstractmethod
    def delete(self, id_):
        pass


class IArticleRepository(ABC):

    @abstractmethod
    def get_all(self):
        pass

    @abstractmethod
    def get_all_names(self):
        pass

    @abstractmethod
    def get_indexed_articles(self):
        pass

    @abstractmethod
    def get_second_indexed_article(self):
        pass

    @abstractmethod
    def get_third_indexed_article(self):
        pass

    @abstractmethod
    def get_most_viewed_articles(self):
        pass

    @abstractmethod
    def increase_view_count(self, id_):
        pass

    @abstractmethod
    def get_recent_articles(self):
        pass

    @abstractmethod
    def get_articles_in_type(self, type_):
        pass

    @abstractmethod
    def get_by_id(self, id_):
        pass

    @abstractmethod
    def get_by_tag(self, tag):
        pass

    @abstractmethod
    def get_by_category(self, category):
        pass

    @abstractmethod
    def create(self, article):
        pass

    @abstractmethod
    def update(self, id_, article):
        pass

    @abstractmethod
    def delete(self, id_):
        pass


class ICommentRepository(ABC):

    @abstractmethod
    def get_all(self):
        pass

    @abstractmethod
    def get_by_id(self, id_):
        pass

    @abstractmethod
    def get_by_post(self, post_id):
        pass

    @abstractmethod
    def create(self, comment_data):
        pass

    @abstractmethod
    def update(self, id_, comment_data):
        pass

    @abstractmethod
    def delete(self, id_):
        pass
