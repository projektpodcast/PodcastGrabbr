<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
    xmlns:atom="http://www.w3.org/2005/Atom"
    xmlns:itunes1="http://www.itunes.com/dtds/podcast-1.0.dtd"
    xmlns:itunes2="http://www.itunes.com/DTDs/Podcast-1.0.dtd">
  <xsl:output method="xml" indent="yes"/>

  <!--/// AUTHOR DER KLASSE: PG
  ///-->

  <xsl:template match="/rss">
    <xsl:element name="podcasts">
      <xsl:apply-templates select="channel"/>
    </xsl:element>
  </xsl:template>

  <xsl:template match="channel">
    <xsl:element name="show">

      <xsl:attribute name="sid">
        <xsl:text>new episode</xsl:text>
      </xsl:attribute>

      <xsl:choose>
        <xsl:when test="managingEditor != ''">
          <xsl:element name="publisher">
            <xsl:value-of select="managingEditor"/>
          </xsl:element>
        </xsl:when>
        <xsl:otherwise>
          <xsl:if test="not(normalize-space(itunes1:author)='')">
            <xsl:element name="publisher">
              <xsl:value-of select="itunes1:author"/>
            </xsl:element>
          </xsl:if>
          <xsl:if test="not(normalize-space(itunes2:author)='')">
            <xsl:element name="publisher">
              <xsl:value-of select="itunes2:author"/>
            </xsl:element>
          </xsl:if>
        </xsl:otherwise>
      </xsl:choose>

      <xsl:element name="title">
        <xsl:value-of select="title"/>
      </xsl:element>

      <xsl:element name="subtitle">
        <xsl:value-of select="subtitle"/>
      </xsl:element>

      <xsl:element name="language">
        <xsl:value-of select="language"/>
      </xsl:element>

      <xsl:element name="description">
        <xsl:value-of select="description"/>
      </xsl:element>

      <xsl:element name="lastupdated">
        <xsl:value-of select="pubDate"/>
      </xsl:element>

      <xsl:element name="lastbuilddate">
        <xsl:value-of select="lastBuildDate"/>
      </xsl:element>


      <xsl:choose>
        <xsl:when test="not(normalize-space(itunes1:image/@href)='')">
          <xsl:element name="imageuri">
            <xsl:value-of select="itunes1:image/@href"/>
          </xsl:element>
        </xsl:when>
        <xsl:when test="not(normalize-space(itunes2:image/@href)='')">
          <xsl:element name="imageuri">
            <xsl:value-of select="itunes2:image/@href"/>
          </xsl:element>
        </xsl:when>
        <xsl:otherwise>
          <xsl:element name="imageuri">
            <xsl:text>'no image available'</xsl:text>
          </xsl:element>
        </xsl:otherwise>
      </xsl:choose>

      <!--<xsl:choose>
        <xsl:when test="itunes:image/@href != ''">
          <xsl:element name="imageuri">
            <xsl:value-of select="itunes:image/@href"/>
          </xsl:element>
        </xsl:when>
        <xsl:otherwise>
          <xsl:element name="imageuri">
            <xsl:text>'no image available'</xsl:text>
          </xsl:element>
        </xsl:otherwise>
      </xsl:choose>-->

      <xsl:element name="link">
        <xsl:value-of select="atom:link/@href"/>
      </xsl:element>


      <xsl:element name="allepisodes">
        <xsl:apply-templates select="item"></xsl:apply-templates>
      </xsl:element>

    </xsl:element>
  </xsl:template>


  <xsl:template match="item">


    <xsl:variable name="episodeidcounter" select="count(following-sibling::item) + 1" />

    <xsl:element name="episode">

      <xsl:attribute name="eid">
        <xsl:value-of select="$episodeidcounter"/>
      </xsl:attribute>

      <xsl:element name="title">
        <xsl:apply-templates select="title"/>
      </xsl:element>
      <xsl:element name="pubdate">
        <xsl:apply-templates select="pubDate"/>
      </xsl:element>

      <xsl:element name="description">
        <xsl:choose>
          <xsl:when test="description != ''">
            <xsl:apply-templates select="description"/>
          </xsl:when>
          <xsl:otherwise>
            <xsl:if test="not(normalize-space(itunes1:summary)='')">
                <xsl:value-of select="itunes1:summary"/>
            </xsl:if>
            <xsl:if test="not(normalize-space(itunes2:summary)='')">
                <xsl:value-of select="itunes2:summary"/>
            </xsl:if>
          </xsl:otherwise>
        </xsl:choose>
      </xsl:element>

      <xsl:element name="url">
        <xsl:apply-templates select="enclosure/@url"/>
      </xsl:element>

      <xsl:element name="length">
        <xsl:apply-templates select="length"/>
      </xsl:element>

      <xsl:element name="localpath">
      </xsl:element>

    </xsl:element>
  </xsl:template>


</xsl:stylesheet>
