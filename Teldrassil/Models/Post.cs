using System;
namespace Teldrassil.Models;

public class Post
{
    /// <summary>
    /// 文章ID
    /// </summary>
    public int Id { set; get; }
    /// <summary>
    /// 文章标题
    /// </summary>
    public string? Title { set; get; }
    /// <summary>
    /// 概要内容
    /// </summary>
    public string? Summary { set; get; }
    /// <summary>
    /// 是否发布（否则为草稿状态）
    /// </summary>
    public bool IsPublish { set; get; }
    /// <summary>
    /// 文章内容（markdown格式）
    /// </summary>
    public string? Content { set; get; }
    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreationTime { set; get; }
    /// <summary>
    /// 最后更新时间
    /// </summary>
    public DateTime LastUpdateTime { set; get; }
    /// <summary>
    /// 分类ID
    /// </summary>
    public int CategoryId { set; get; }
    /// <summary>
    /// 分类
    /// </summary>
    public Category? Category { set; get; }
    /// <summary>
    /// 分类层级（类似3，2，1），用代码把该字段内容处理成List<Category>就可以展示分类层级
    /// </summary>
    public string? Categories { set; get; }

}

