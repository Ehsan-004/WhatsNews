from django.shortcuts import render
from django.views import View
from django.views.generic.list import ListView
from django.views.generic import DetailView
from .Data.repository import CategoryRepository, ArticleRepository, TagRepository, CommentRepository
from django.http import JsonResponse
# search part
from sklearn.feature_extraction.text import TfidfVectorizer
from sklearn.metrics.pairwise import cosine_similarity




class IndexView(View):
    template_name = 'index.html'
    def get(self, request):
        indexed_articles = ArticleRepository.get_indexed_articles(request)
        categories = CategoryRepository.get_all_with_post_count(request)
        recent_articles = ArticleRepository.get_recent_articles(request)
        most_viewed_article = ArticleRepository.get_most_viewed_articles(request)
        second_indexed_article = ArticleRepository.get_second_indexed_article(request)
        third_indexed_article = ArticleRepository.get_third_indexed_article(request)

        # عکس یادداشت گزارش گفتگو
        pictural_articles = ArticleRepository.get_articles_in_type(request, 'P')
        interview_articles = ArticleRepository.get_articles_in_type(request, 'I')  # گزارش
        report_articles = ArticleRepository.get_articles_in_type(request, 'R')  # یادداشت
        conversation_articles = ArticleRepository.get_articles_in_type(request, 'C')  # گفتگو

        context = {
            'indexed_articles': indexed_articles,
            'most_viewed_article': most_viewed_article,
            'second_indexed_article': second_indexed_article,
            'third_indexed_article': third_indexed_article,
            'categories': categories,
            'recent_articles': recent_articles,
            'pictural_articles': pictural_articles,
            'interview_articles': interview_articles,
            'report_articles': report_articles,
            'conversation_articles': conversation_articles,
        }
        return render(request, self.template_name, context)


class SingleArticleView(DetailView):
    context_object_name = 'post'
    template_name = 'single.html'

    def get_object(self, queryset=None):
        id_ = self.kwargs.get('pk')
        post = ArticleRepository.get_by_id(self.request, id_)
        post.view_count += 1
        return post

    def get_context_data(self, **kwargs):
        context = super().get_context_data(**kwargs)
        id_ = self.kwargs.get('pk')
        post = ArticleRepository.get_by_id(self.request, id_)
        comments = CommentRepository.get_by_post(self.request, post)
        context['comments'] = comments
        return context


class AllArticlesView(ListView):
    context_object_name = 'articles'
    template_name = 'blog.html'
    paginate_by = 16

    def get_queryset(self):
        return ArticleRepository.get_all(self.request)


class CategoryArticlesView(ListView):
    template_name = 'category.html'
    context_object_name = 'articles'
    paginate_by = 9

    def get_queryset(self):
        id_ = self.kwargs.get('pk')
        category = CategoryRepository.get_by_id(self.request, id_)
        return ArticleRepository.get_by_category(self.request, category)

    def get_context_data(self, **kwargs):
        context = super().get_context_data(**kwargs)
        id_ = self.kwargs.get('pk')
        category = CategoryRepository.get_by_id(self.request, id_)
        categories = CategoryRepository.get_all_with_post_count(self.request)
        recent_articles = ArticleRepository.get_recent_articles(self.request)
        context['category_name'] = category.name
        context['categories'] = categories
        context['recent_articles'] = recent_articles
        return context


def add_comment(request):
    # if not request.user and not request.user.is_authenticated():
        # return "You are not an admin!"

    if request.method == 'POST':
        auther_name = request.POST.get('author_name', False)
        auther_email = request.POST.get('author_email', False)
        content = request.POST.get('content', False)
        post_id = request.POST.get('post_id', False)
        if False in [auther_name, auther_email, content, post_id]:
            return JsonResponse({'error': 'Fill all the fields.'}, status=400)
        ar = ArticleRepository()
        post = ar.get_by_id(post_id)

        comment_data = {
            'auther_name': auther_name,
            'auther_email': auther_email,
            'comment': content,
            'post': post,
        }
        cr = CommentRepository()
        status = cr.create(comment_data)

        if status >= 0:
            comment = CommentRepository.get_by_id(request, status)
            return JsonResponse({
                'auther_name': comment.auther_name,
                'auther_email': comment.auther_email,
                'comment': comment.comment,
            })
        else: return JsonResponse({'error': 'Failed to create comment'}, status=400)
    else:
        print('got a post request')
        return JsonResponse({'error': 'Invalid request method'}, status=405)


def about_us(request):
    if request.method == "GET":
        return render(request, 'about-us.html')


def search(request):
    if request.method == "GET":
        articles = ArticleRepository.get_all_names(request)
        query = request.GET.get('query')

        vectorizer = TfidfVectorizer()
        tfidf_matrix = vectorizer.fit_transform(articles)
        query_vector = vectorizer.transform([query])
        similarities = cosine_similarity(query_vector, tfidf_matrix).flatten()
        ranked_indices = similarities.argsort()[::-1]

        context = {}
        for idx in ranked_indices:
            print(f"مقاله: {articles[idx]}, شباهت: {similarities[idx]:.2f}")
            context[articles[idx]] = similarities[idx]
        return JsonResponse(**context)


def handler404(request, exception):
    return render(request, 'p404.html', status=404)
