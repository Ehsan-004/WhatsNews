from django.conf.urls.static import static
from django.urls import path
import News.views as views
from WhatsNews.settings import MEDIA_ROOT, MEDIA_URL, DEBUG


app_name = 'News'


urlpatterns = [
    path('', views.IndexView.as_view(), name='index'),
    path('article/<int:pk>/', views.SingleArticleView.as_view(), name='single'),
    path('news/', views.AllArticlesView.as_view(), name='all'),
    path('category/<int:pk>/', views.CategoryArticlesView.as_view(), name='category'),
    path('add_comment/', views.add_comment, name='add_comment'),
    path('about_us/', views.about_us, name='about_us'),
    path('contact_us/', views.about_us, name='contact_us'),
    path('search/', views.search, name='search'),
]

if DEBUG:
    urlpatterns += static(MEDIA_URL, document_root=MEDIA_ROOT)

handler404 = 'News.views.handler404'
