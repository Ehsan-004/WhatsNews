from django.contrib import admin
from .models import Category, Tag, Article, Comment

class CategoryAdmin(admin.ModelAdmin):
    list_display = ('name',)

class TagAdmin(admin.ModelAdmin):
    list_display = ('name',)

class ArticleAdmin(admin.ModelAdmin):
    list_display = ('title', 'create_date',)

class CommentAdmin(admin.ModelAdmin):
    list_display = ('post', 'auther_name', 'auther_email', 'comment',)

admin.site.register(Category, CategoryAdmin)
admin.site.register(Tag, TagAdmin)
admin.site.register(Article, ArticleAdmin)
admin.site.register(Comment, CommentAdmin)
